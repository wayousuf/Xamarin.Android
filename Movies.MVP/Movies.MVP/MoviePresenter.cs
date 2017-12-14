using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Movies.MVP.Model;

namespace Movies.MVP
{
    public class MoviePresenter
    {
        private CancellationTokenSource _cts;

        public event Action<IReadOnlyList<Movie>> FilterApplied;

        public async Task FilterMoviesAsync(string search)
        {
            _cts?.Cancel();

            if (!string.IsNullOrEmpty(search))
            {
                var innerToken = _cts = new CancellationTokenSource();
                var movieService = new MovieService();
                var movies = await movieService.GetMoviesForSearchAsync(search);

                if (!innerToken.IsCancellationRequested)
                    FilterApplied?.Invoke(movies);
            }
            else
            {
                FilterApplied?.Invoke(null);
            }
        }
    }
}