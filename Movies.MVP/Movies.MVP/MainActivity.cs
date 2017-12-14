using Android.App;
using Android.Widget;
using Android.OS;

namespace Movies.MVP
{
    [Activity(Label = "Movies.MVP", MainLauncher = true)]
    public class MainActivity : Activity
    {
        MoviePresenter _presenter;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var movieList = FindViewById<ListView>(Resource.Id.movieList);
            var adapter = new MovieAdapter();
            movieList.Adapter = adapter;

            _presenter = new MoviePresenter();
            _presenter.FilterApplied += adapter.SetData;


            var searchView = FindViewById<SearchView>(Resource.Id.searchView);
            searchView.QueryTextChange += OnSearch;
        }

        private async void OnSearch(object sender, SearchView.QueryTextChangeEventArgs e)
        {
            await _presenter.FilterMoviesAsync(e.NewText);
        }
    }
}

