using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebTokenManagmentSystem.Models;

namespace WebTokenManagmentSystem.Service
{
    public class TicketSpeaker : IHostedService, IDisposable
    {

        private Timer _timer;
        private static bool isAccouning = false;
        private WebTokenManagmentSystemDBContext context = new WebTokenManagmentSystemDBContext();



        public void Dispose()
        {
            _timer.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Debug.WriteLine("Timed Background Service is on");
          
        
                _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
          
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Debug.WriteLine("Timed Background Service is off");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
       
            Debug.WriteLine("Timed Background Service is working");
            var AnnoucmentRow = context.QueueHistories.Where(x => x.IsPlayed == false).FirstOrDefault();
            if (AnnoucmentRow != null && !isAccouning)
            {
                isAccouning = true;
                Debug.WriteLine("New Accoument");
                var speech = new System.Speech.Synthesis.SpeechSynthesizer();
                speech.SelectVoice("Microsoft Zira Desktop");
                speech.Rate = -1;
                speech.Speak($"Ticket number {AnnoucmentRow.TokenNumber} please proceed to counter number {AnnoucmentRow.CounterId}");
                AnnoucmentRow.IsPlayed = true;
                context.SaveChanges();
                isAccouning = false;
            }
        
        }
    }
}
