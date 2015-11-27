using System;
using Android.Widget;
using Android.Content;
using Android.Graphics;
using System.Collections.Generic;

namespace MLearning.Droid
{
	public class Template3: RelativeLayout
	{

		RelativeLayout mainLayout;
		LinearLayout contenLayout;


		int widthInDp;
		int heightInDp;

		Context context;


		LinearLayout contentListLayout;
		TextView titleHeaderList;
		ListView contentList;
		ImageView imBorderList;
		List<TemplateItem> _dataTemplateItem = new List<TemplateItem> ();
		String icon = "icons/circulob.png";
		public Template3 (Context context) :
		base (context)
		{
			this.context = context;
			Initialize ();
		}

		void Initialize ()
		{
			var metrics = Resources.DisplayMetrics;
			widthInDp = ((int)metrics.WidthPixels);
			heightInDp = ((int)metrics.HeightPixels);
			Configuration.setWidthPixel (widthInDp);
			Configuration.setHeigthPixel (heightInDp);

			ini ();
		
			this.AddView (mainLayout);

		}


		public Bitmap getBitmapFromAsset( String filePath) {
			System.IO.Stream s = context.Assets.Open (filePath);
			Bitmap bitmap = BitmapFactory.DecodeStream (s);

			return bitmap;
		}

		public void ini(){


			var textFormat = Android.Util.ComplexUnitType.Px;

			mainLayout = new RelativeLayout (context);
			mainLayout.LayoutParameters = new RelativeLayout.LayoutParams (-1,-1);


			contenLayout = new LinearLayout (context);
			contenLayout.LayoutParameters = new LinearLayout.LayoutParams (-2, -2);
			contenLayout.Orientation = Orientation.Vertical;


			//LIST

			contentListLayout = new LinearLayout (context);
			contentListLayout.LayoutParameters = new LinearLayout.LayoutParams (Configuration.getWidth(583),Configuration.getHeight(300));
			contentListLayout.Orientation = Orientation.Vertical;
			titleHeaderList = new TextView (context);
			contentList = new ListView (context);

			//titleHeaderList.Text = "Tipos de Aves";
			titleHeaderList.SetTextColor (Color.ParseColor ("#FF0080"));
			titleHeaderList.SetTextSize (textFormat, Configuration.getHeight (38));
			titleHeaderList.SetMaxWidth (Configuration.getWidth (510));

			contentListLayout.SetBackgroundResource (Resource.Drawable.border);
			contentListLayout.AddView (titleHeaderList);
			contentListLayout.AddView (contentList);


			contentListLayout.SetX (Configuration.getHeight (25));
			//contentListLayout.SetY (Configuration.getWidth (450));


			//ENDLIST

			imBorderList = new ImageView (context);
			mainLayout.AddView (contentListLayout);
		}

		private string _title;
		public string Title{
			get{return _title; }
			set{_title = value;
				titleHeaderList.Text = _title;}

		}

		private string[] _listItems;
		public string[] ListItems{
			set{_listItems = value;
				for (int i = 0; i < _listItems.Length; i++) {
					_dataTemplateItem.Add (new TemplateItem (){ im_vinheta = icon, content = _listItems[i]});

				}
				contentList.Adapter = new TemplateAdapter (context, _dataTemplateItem);
				contentList.SetBackgroundColor (Color.White);
				contentList.DividerHeight = 0;
				contentList.Clickable = false;
				contentList.ChoiceMode = ChoiceMode.None;

			}

		}


	}
}

