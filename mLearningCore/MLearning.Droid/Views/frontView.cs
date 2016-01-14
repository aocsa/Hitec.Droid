
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using Android.Graphics.Drawables;
using Square.Picasso;

namespace MLearning.Droid
{
	public class frontView : RelativeLayout
	{

		RelativeLayout _mainLayout;

		LinearLayout _linearContentLayout;
		VerticalScrollView _scrollItems;
		LinearLayout _publicidadLayout;
		LinearLayout _adLayout;

		public List<LinearLayout> _listLinearItem = new List<LinearLayout>();

		public List<Drawable> _coverImages = new List<Drawable> ();

		int widthInDp;
		int heightInDp;

		public List<string> adsImagesPath = new List<string>();
		public bool adOpen = false;

		Context context;

		public frontView (Context context) :
			base (context)
		{
			this.context = context;
			Initialize ();
		}

		public frontView (Context context, IAttributeSet attrs) :
			base (context, attrs)
		{
			Initialize ();
		}

		public frontView (Context context, IAttributeSet attrs, int defStyle) :
			base (context, attrs, defStyle)
		{
			Initialize ();
		}

		void Initialize ()
		{
			var metrics = Resources.DisplayMetrics;
			widthInDp = ((int)metrics.WidthPixels);
			heightInDp = ((int)metrics.HeightPixels);
			Configuration.setWidthPixel (widthInDp);
			Configuration.setHeigthPixel (heightInDp);

			foreach (var w in AddResources.Instance.addList)
				adsImagesPath.Add (w);
	
			initUi ();
			this.AddView (_mainLayout);
		}

		void showAd(int idAd)
		{

			adOpen = true;
			_adLayout = new LinearLayout (context);
			_adLayout.LayoutParameters = new LinearLayout.LayoutParams (-1, Configuration.getHeight (255));


			//Drawable dr = new BitmapDrawable (getBitmapFromAsset (adsImagesPath[idAd]));
			//_adLayout.SetBackgroundDrawable (dr);
			//_adLayout.SetY (Configuration.getHeight(1136-85-255));

			ImageView imgProfile = new ImageView (context);
			imgProfile.LayoutParameters = new LinearLayout.LayoutParams (-1, Configuration.getHeight (255));
			Picasso.With (context).Load (adsImagesPath[idAd]).Resize(Configuration.getWidth(640),Configuration.getHeight(255)).Into (imgProfile);
			imgProfile.SetY (Configuration.getHeight (1136 - 85 - 255));

			_mainLayout.AddView (imgProfile);

			imgProfile.Click += delegate {
				String url = "https://www.facebook.com/HiTecPe";
				Intent i = new Intent (Intent.ActionView);
				i.SetData (Android.Net.Uri.Parse (url));
				context.StartActivity(i);
			};
		}

		void hideAd()
		{
			adOpen = false;
			int numAd = _mainLayout.ChildCount;
			_mainLayout.RemoveViewAt (numAd-1);
		}

		public Bitmap getBitmapFromAsset( String filePath) {
			System.IO.Stream s = context.Assets.Open (filePath);
			Bitmap bitmap = BitmapFactory.DecodeStream (s);

			return bitmap;
		}

		private void initUi()
		{
			_mainLayout = new RelativeLayout (context);
			_mainLayout.LayoutParameters = new RelativeLayout.LayoutParams (-1, -1);

			_scrollItems = new VerticalScrollView (context);
			_scrollItems.LayoutParameters = new VerticalScrollView.LayoutParams (-1, Configuration.getHeight(965));
			_scrollItems.SetY (Configuration.getHeight (125));

			initItems ();
			_scrollItems.AddView (_linearContentLayout);

			_mainLayout.AddView (_scrollItems);
			_publicidadLayout = new LinearLayout (context);
			_publicidadLayout.LayoutParameters = new LinearLayout.LayoutParams (-1, Configuration.getHeight (85));
			Drawable dr = new BitmapDrawable (getBitmapFromAsset ("images/footerad.jpg"));
			_publicidadLayout.SetBackgroundDrawable (dr);
			_publicidadLayout.SetY (Configuration.getHeight(1136-85));
			_mainLayout.AddView (_publicidadLayout);
			_publicidadLayout.Click += delegate {
				if (adOpen) {
					

					hideAd ();
				} else {
					Random rnd = new Random();
					int nextval = rnd.Next(0, 7);
					showAd (nextval);
				}
			};


		}

		private void initItems()
		{

			var textFormat = Android.Util.ComplexUnitType.Px;

			_linearContentLayout = new LinearLayout (context);
			_linearContentLayout.LayoutParameters = new LinearLayout.LayoutParams (-1, -2);
			_linearContentLayout.Orientation = Orientation.Vertical;


			List<string> title = new List<string> ();
			List<string> type = new List<string> ();
			List<string> coverImagePath = new List<string> ();
			List<string> numLikes = new List<string> ();
			List<string> numTypes = new List<string> ();

			numLikes.Add ("10");
			numLikes.Add ("10");
			numLikes.Add ("10");
			numLikes.Add ("10");

			numTypes.Add ("3");
			numTypes.Add ("7");
			numTypes.Add ("4");
			numTypes.Add ("9");

			title.Add ("LAS RUTAS");
			title.Add ("GUIA DE SERVICIOS");
			title.Add ("GUIA DE IDENTIFICACION SILVESTRE");
			title.Add ("CAMINO INCA EN CIFRAS");

			type.Add ("rutas");
			type.Add ("guias");
			type.Add ("guias");
			type.Add ("cifras");

			coverImagePath.Add ("images/fondorutas.png");
			coverImagePath.Add ("images/fondoguias.png");
			coverImagePath.Add ("images/fondovidasilvestre.png");
			coverImagePath.Add ("images/fondocaminoinca.png");

			int heightItem = Configuration.getHeight (310);

			//Bitmap likeBitmap = Bitmap.CreateScaledBitmap (getBitmapFromAsset ("icons/like.png"), Configuration.getWidth (30), Configuration.getWidth (30), true);


			for (int i = 0; i < title.Count; i++) 
			{
				LinearLayout item = new LinearLayout (context);
				item.LayoutParameters = new LinearLayout.LayoutParams (-1, heightItem);
				item.Orientation = Orientation.Horizontal;
				item.SetGravity (GravityFlags.Center);

				Drawable cover = new BitmapDrawable (getBitmapFromAsset(coverImagePath[i]));
				item.SetBackgroundDrawable (cover);
				_coverImages.Add (cover);

				TextView itemTitle = new TextView (context);
				itemTitle.Text = title [i];
				itemTitle.SetTextColor (Color.ParseColor("#ffffff"));
				itemTitle.Typeface =  Typeface.CreateFromAsset(context.Assets, "fonts/ArcherMediumPro.otf");
				itemTitle.SetTextSize (ComplexUnitType.Fraction, Configuration.getHeight(45));

				LinearLayout linearTitle = new LinearLayout (context);
				linearTitle.LayoutParameters = new LinearLayout.LayoutParams(Configuration.getWidth(465),Configuration.getHeight(180));
				linearTitle.Orientation = Orientation.Vertical;
				linearTitle.SetGravity (GravityFlags.Center);

				linearTitle.AddView (itemTitle);

				ImageView iconlike = new ImageView (context);
				//iconlike.SetImageBitmap(likeBitmap);


				LinearLayout linearLike = new LinearLayout (context);
				linearLike.LayoutParameters = new LinearLayout.LayoutParams (-1, -2);
				linearLike.Orientation = Orientation.Vertical;
				linearLike.SetGravity (GravityFlags.CenterHorizontal);

				TextView txtnumLike = new TextView (context);
				txtnumLike.Text = numLikes[i];
				txtnumLike.Gravity = GravityFlags.CenterHorizontal;
				txtnumLike.SetTextColor (Color.ParseColor ("#ffffff"));


				//linearLike.AddView (iconlike);
				//linearLike.AddView (txtnumLike);


				LinearLayout linearType = new LinearLayout (context);
				linearType.LayoutParameters = new LinearLayout.LayoutParams (-1, -2);
				linearType.Orientation = Orientation.Vertical;
				linearType.SetGravity (GravityFlags.CenterHorizontal);

				TextView txtnumType = new TextView (context);
				txtnumType.Text = numTypes[i];
				txtnumType.Typeface =  Typeface.CreateFromAsset(context.Assets, "fonts/ArcherMediumPro.otf");
				txtnumType.TextSize = Configuration.getHeight (15);
				txtnumType.Gravity = GravityFlags.CenterHorizontal;
				txtnumType.SetTextColor (Color.ParseColor ("#ffffff"));

				TextView txtType = new TextView (context);
				txtType.Text = type[i];
				txtType.Typeface =  Typeface.CreateFromAsset(context.Assets, "fonts/ArcherMediumPro.otf");
				txtType.TextSize = Configuration.getHeight (15);
				txtType.Gravity = GravityFlags.CenterHorizontal;
				txtType.SetTextColor (Color.ParseColor ("#ffffff"));

				int space = Configuration.getHeight (20);

				linearLike.SetPadding (0, 0, 0, space);
				linearType.SetPadding (space, 0, 0, 0);


				linearType.AddView (txtnumType);
				linearType.AddView (txtType);







				LinearLayout linearExtraInfo = new LinearLayout (context);
				linearExtraInfo.LayoutParameters = new LinearLayout.LayoutParams (Configuration.getWidth(100), -2);
				linearExtraInfo.Orientation = Orientation.Vertical;
				linearExtraInfo.SetGravity (GravityFlags.CenterHorizontal);

				linearExtraInfo.AddView (linearLike);
				linearExtraInfo.AddView (linearType);

				item.AddView (linearTitle);
				//item.AddView (linearExtraInfo);

				_listLinearItem.Add (item);
				_linearContentLayout.AddView (_listLinearItem [i]);

			}

		}

	}
}

