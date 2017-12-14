using System.Collections.Generic;
using Android.Views;
using Android.Widget;
using Movies.MVP.Model;

namespace Movies.MVP
{
    internal class MovieAdapter : BaseAdapter<Movie>
    {
        IReadOnlyList<Movie> _movies;

        public void SetData(IReadOnlyList<Movie> movies)
        {
            this._movies = movies;
            this.NotifyDataSetInvalidated();
        }

        public override Movie this[int position] => _movies[position];

        public override int Count => _movies?.Count ?? 0;

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            Movie movie = _movies[position];

            ViewHolder holder;
            var view = convertView;

            if (view == null)
            {
                var context = parent.Context;
                var inflator = LayoutInflater.FromContext(context);

                var layout = inflator.Inflate(Android.Resource.Layout.SimpleListItem2, null);
                var titleText = layout.FindViewById<TextView>(Android.Resource.Id.Text1);
                var descriptionText = layout.FindViewById<TextView>(Android.Resource.Id.Text2);

                descriptionText.SetMaxLines(1);
                descriptionText.Ellipsize = Android.Text.TextUtils.TruncateAt.End;

                holder = new ViewHolder { Title = titleText, Description = descriptionText };
                layout.Tag = holder;
                view = layout;
            }
            else
            {
                holder = (ViewHolder)view.Tag;
            }

            holder.Title.Text = movie.Title;
            holder.Description.Text = movie.Description;

            return view;
        }
    }

    internal class ViewHolder : Java.Lang.Object
    {
        public TextView Title { get; set; }
        public TextView Description { get; set; }
    }
}