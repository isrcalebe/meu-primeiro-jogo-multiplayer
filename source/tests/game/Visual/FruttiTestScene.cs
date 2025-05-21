namespace Frutti.Game.Tests.Visual;

public abstract partial class FruttiTestScene : TestScene
{
    protected override ITestSceneTestRunner CreateRunner()
        => new FruttiTestRunner();

    private partial class FruttiTestRunner : FruttiGameBase, ITestSceneTestRunner
    {
        private TestSceneTestRunner.TestRunner? runner;

        protected override void LoadAsyncComplete()
        {
            base.LoadAsyncComplete();

            Add(runner = new TestSceneTestRunner.TestRunner());
        }

        public void RunTestBlocking(TestScene test)
            => runner?.RunTestBlocking(test);
    }
}
