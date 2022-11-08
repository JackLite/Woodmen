using Woodman.Utils;

namespace Woodman.Logs
{
    public class LogsInnerPool : Pool<LogView>
    {
        protected override void ResetPoolObject(LogView view)
        {
            view.Hide();
        }

        protected override void OnBeforeGet(LogView _)
        {
        }
    }
}