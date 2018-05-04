using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gamex.src.Util.Coordinate;

namespace Gamex.src.testland
{
    class CoordinateTest : GamexTest
    {
        private Random RNG = new Random();

        public TestResult Run()
        {
            var result = new TestResult("Coordinate Tests");
            
            result.LogResult(CoordinateTestCasting());

            return result;
        }

        private TestResult CoordinateTestCasting()
        {
            var result = new TestResult();

            result.LogResult(CoordinateTestCastingToGameCoordinate());
            result.LogResult(CoordinateTestCastingToPixelCoordinate());
            result.LogResult(CoordinateTestCastingToGLCoordinate());
            result.LogResult(CoordinateTestCastingPixelToGL());
            result.LogResult(CoordinateTestGameToGLWithCamera());
            

            return result;
        }

        private TestResult CoordinateTestGameToGLWithCamera()
        {
            var tolerance = 0.01f;

            var result = new TestResult();

            var camera = new GameCoordinate(5, 5);
            var loc = new GameCoordinate(5, 5);
            var facit = new GLCoordinate(0, 0);
            var gl = loc.ToGLCoordinate(camera);

            if (!gl.CloseEnough(facit, tolerance))
            {
                result.LogFailureInCase(1);
            }


            camera = new GameCoordinate(2, 2);
            loc = new GameCoordinate(4, 4);
            facit = new GLCoordinate(2, -2);
            gl = loc.ToGLCoordinate(camera);

            if (!gl.CloseEnough(facit, tolerance))
            {
                result.LogFailureInCase(2);
            }

            return result;
        }

        private TestResult CoordinateTestCastingPixelToGL()
        {
            var runs = 100;
            var tolerance = 0.02f;

            var result = new TestResult();

            for (int j = 0; j < runs; j++)
            {
                Size windowSize = new Size(RNG.Next(100, 1000), RNG.Next(100, 1000));

                var pixelPoint = new PixelCoordinate(RNG.Next(windowSize.Width), RNG.Next(windowSize.Height));
                var glPoint = pixelPoint.ToGLCoordinate(windowSize);

                var xpercent = (pixelPoint.X/(float)windowSize.Width);
                var ypercent = (pixelPoint.Y/(float)windowSize.Height);

                var xanswer = (xpercent - 0.5f)*2;
                var yanswer = (ypercent - 0.5f)*2;

                var answer = new GLCoordinate(xanswer, yanswer);

                if (!answer.CloseEnough(glPoint, tolerance))
                {
                    result.LogFailureInCase(windowSize);
                }
            }

            return result;
        }

        private TestResult CoordinateTestCastingToGameCoordinate()
        {
            Func<GLCoordinate, GLCoordinate> transform = (coord) =>
            {
                var randomCoord = GenerateRandomGLCoordinate();
                var cameraLocation = new GameCoordinate(randomCoord.X, randomCoord.Y);

                var gamecoordinate = GameCoordinate.FromGLCoordinate(coord, cameraLocation);
                var recast = gamecoordinate.ToGLCoordinate(cameraLocation);
                return recast;
            };
            
            return CoordinateTestCastingLoop(new TestResult(), transform);
        }

        private TestResult CoordinateTestCastingToPixelCoordinate()
        {
            Func<GLCoordinate, GLCoordinate> transform = (coord) =>
            {
                Size windowSize = new Size(RNG.Next(10, 1000), RNG.Next(10, 1000));

                var pixelCoordinate = PixelCoordinate.FromGLCoordinate(coord, windowSize);
                var recast = pixelCoordinate.ToGLCoordinate(windowSize);

                return recast;
            };

            return CoordinateTestCastingLoop(new TestResult(), transform);
        }

        private TestResult CoordinateTestCastingToGLCoordinate()
        {
            Func<GLCoordinate, GLCoordinate> transform = (coord) => new GLCoordinate(coord);
            return CoordinateTestCastingLoop(new TestResult(), transform);
        }

        private TestResult CoordinateTestCastingLoop(TestResult result, Func<GLCoordinate, GLCoordinate> transform)
        {
            var runs = 100;
            var tolerance = 0.2f;

            GLCoordinate c1;

            for (int i = 0; i < runs; i++)
            {
                c1 = GenerateRandomGLCoordinate();
                var c1clone = transform(c1);

                if (!c1.CloseEnough(c1clone, tolerance))
                {
                    result.LogFailureInCase(c1);
                    break;
                }
            }

            return result;
        }

        private GLCoordinate GenerateRandomGLCoordinate(float minMaxValue = 100f)
        {
            var x = (RNG.NextDouble() - 0.5)* minMaxValue * 2;
            var y = (RNG.NextDouble() - 0.5)* minMaxValue * 2;

            return new GLCoordinate((float)x, (float)y);
        }
    }
}
