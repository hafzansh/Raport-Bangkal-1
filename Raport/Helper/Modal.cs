using Raport.Helper.ModalUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Raport.Helper
{
    class Modal
    {
        public static void Spinner(Action<IProgress<string>> work)
        {
            Spinner splash = new Spinner();

            splash.Loaded += (_, args) =>
            {
                BackgroundWorker worker = new BackgroundWorker();
                Progress<string> progress = new Progress<string>(
                    data => splash.Title = data);

                worker.DoWork += (s, workerArgs) => work(progress);
                worker.RunWorkerCompleted +=
                    (s, workerArgs) => splash.Close();

                worker.RunWorkerAsync();
            };

            splash.ShowDialog();
        }
        public static void ProgressModal(Action<IProgress<string>> work)
        {
            ProgressModal splash = new ProgressModal();

            splash.Loaded += (_, args) =>
            {
                BackgroundWorker worker = new BackgroundWorker();
                Progress<string> progress = new Progress<string>(
                    data => splash.pValue.Content = data);

                worker.DoWork += (s, workerArgs) => work(progress);
                worker.RunWorkerCompleted +=
                    (s, workerArgs) => splash.Close();

                worker.RunWorkerAsync();
            };

            splash.ShowDialog();
        }
    }
}
