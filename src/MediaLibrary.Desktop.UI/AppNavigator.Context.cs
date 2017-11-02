using MediaLibrary.ViewModels.Services;
using Neptuo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MediaLibrary
{
    partial class AppNavigator
    {
        private class Context<T> : INavigatorContext
        {
            private readonly TaskCompletionSource<T> result;
            private Window window;

            public Context(TaskCompletionSource<T> result)
            {
                Ensure.NotNull(result, "result");
                this.result = result;
            }

            public Context(TaskCompletionSource<T> result, Window window)
                : this(result)
            {
                SetWindow(window);
            }

            public void SetWindow(Window window)
            {
                Ensure.NotNull(window, "window");

                if (this.window != null)
                    this.window.Closed -= OnWindowClosed;

                this.window = window;
                window.Closed += OnWindowClosed;
            }

            private void OnWindowClosed(object sender, EventArgs e)
            {
                if (window != null)
                    window.Closed -= OnWindowClosed;

                Close();
            }

            public void Close()
            {
                Close(null);
            }

            public void Close(object result)
            {
                CloseInternal();
                this.result.SetResult((T)result);
            }

            private void CloseInternal()
            {
                if (window != null)
                {
                    window.Closed -= OnWindowClosed;
                    window.Close();
                }
            }
        }
    }
}
