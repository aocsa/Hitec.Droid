
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
using Android.Text;
using Square.Picasso;
using MLearning.Droid.Views;

namespace MLearning.Droid
{
	public class MapView : RelativeLayout
	{
		RelativeLayout mainLayout;
		LinearLayout mapSpace;
		public VerticalScrollView placeSpace;
		public ImageView mapImage;
		public List<LinearLayout> _placesLayout = new List<LinearLayout>();
		public List<MapItemInfo> _placesData = new List<MapItemInfo> ();
		PopupWindow infoPopUp; 
		LinearLayout fullInfo;

		public List<PlaceItem> _currentPlaces = new List<PlaceItem>();
		public ListView  listPlaces;

		public LinearLayout placesInfoLayout;
		public LinearLayout placesContainer;

		public String titulo;
		public String descripcion;
		public String mapUrl;



		int widthInDp;
		int heightInDp;

		Context context;

		VerticalScrollView scrollPlaces;

		public bool placeInfoOpen = false;

		public MapView (Context context) :
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
			//iniNotifList ();
			this.AddView (mainLayout);

		}


		public Bitmap getBitmapFromAsset( String filePath) {
			System.IO.Stream s = context.Assets.Open (filePath);
			Bitmap bitmap = BitmapFactory.DecodeStream (s);

			return bitmap;
		}

		public void setMapImage(String url)
		{
			
			Picasso.With (context).Load (url).Resize(Configuration.WIDTH_PIXEL,Configuration.getHeight(800)).Placeholder(context.Resources.GetDrawable (Resource.Drawable.progress_animation)).CenterCrop().Into (mapImage);
			mapImage.Click += delegate {
				Intent lance = new Intent();
				lance.SetAction(Intent.ActionView);
				String typedata = "image/*";
				lance.SetType(typedata);
				String phuri = url;
				Android.Net.Uri uri = Android.Net.Uri.Parse(phuri);

				lance.SetDataAndType(uri,typedata);
				context.StartActivity(lance);
			};

		}

		public void ini(){

			placesInfoLayout = new LinearLayout (context);
			placesInfoLayout.LayoutParameters = new LinearLayout.LayoutParams (-1, -2);
			placesInfoLayout.SetPadding(30,30,30,30);
			placesInfoLayout.Orientation = Orientation.Vertical;

			mainLayout = new RelativeLayout (context);
			mainLayout.LayoutParameters = new RelativeLayout.LayoutParams (-1,-1);

			mapImage = new ImageView (context);
			mapSpace = new LinearLayout (context);
			mapSpace.LayoutParameters = new LinearLayout.LayoutParams (-1, Configuration.getHeight(800));
			mapSpace.SetGravity (GravityFlags.CenterHorizontal);
			mapSpace.AddView (mapImage);



			placeSpace = new VerticalScrollView (context);
			placeSpace.LayoutParameters = new LinearLayout.LayoutParams (-1, Configuration.getHeight(345));
			placeSpace.SetY (Configuration.getHeight (800));
			placeSpace.SetBackgroundColor (Color.White);

			placesContainer = new LinearLayout (context);
			placesContainer.LayoutParameters = new LinearLayout.LayoutParams (-1, Configuration.getHeight(345));
			placesContainer.Orientation = Orientation.Vertical;



			/*
			PlaceItem p1 = new PlaceItem{pathIcon="icons/iconcifras.png", titulo = "Piscacucho-Wayllabamba", detalle = "........." };
			PlaceItem p2 = new PlaceItem{pathIcon="icons/iconcifras.png", titulo = "La arquitectura Inca", detalle = "........." };
			PlaceItem p3 = new PlaceItem{pathIcon="icons/iconcifras.png", titulo = "Wayllabamba-Pacaymayo", detalle = "........." };
			PlaceItem p4 = new PlaceItem{pathIcon="icons/iconcifras.png", titulo = "Llaqtapata", detalle = "........." };
			PlaceItem p5 = new PlaceItem{pathIcon="icons/iconcifras.png", titulo = "Miskay", detalle = "........." };
			PlaceItem p6 = new PlaceItem{pathIcon="icons/iconcifras.png", titulo = "Puesto de control Piscacucho (km 82)", detalle = "........." };
			PlaceItem p7 = new PlaceItem{pathIcon="icons/iconcifras.png", titulo = "Campamento Wayllabamba", detalle = "........." };
			PlaceItem p8 = new PlaceItem{pathIcon="icons/iconcifras.png", titulo = "Place", detalle = "........." };

			_currentPlaces.Add (p1);
			_currentPlaces.Add (p2);
			_currentPlaces.Add (p3);
			_currentPlaces.Add (p4);
			_currentPlaces.Add (p4);
			_currentPlaces.Add (p5);
			_currentPlaces.Add (p6);
			_currentPlaces.Add (p7);
			_currentPlaces.Add (p8);
			*/

			mainLayout.AddView (mapSpace);
			mainLayout.AddView (placeSpace);

			scrollPlaces = new VerticalScrollView (context);
			scrollPlaces.LayoutParameters = new VerticalScrollView.LayoutParams (-1,-1);
			scrollPlaces.AddView (placesInfoLayout);


			//mainLayout.AddView (placesInfoLayout);

			//iniPlancesList ();

		}

		public void showPLaceInfo(int position)
		{
			mainLayout.AddView(scrollPlaces);
			scrollPlaces.SetBackgroundColor (Color.White);
			placeInfoOpen = true;
			placesInfoLayout.RemoveAllViews ();

			var extraInfo = _placesData [position].placeExtraInfo;
			for (int i = 0; i < extraInfo.Count; i++) {
				TextView detalle = new TextView (context);
				detalle.Text = extraInfo [i].detalle;

				String url = extraInfo [i].url;
				ImageView image = new ImageView (context);
				Picasso.With (context).Load (url).Placeholder(context.Resources.GetDrawable (Resource.Drawable.progress_animation)).Resize(Configuration.getWidth(640),Configuration.getHeight(640)).CenterInside().Into (image);
				placesInfoLayout.AddView (detalle);
				placesInfoLayout.AddView (image);

			}
			placesInfoLayout.SetBackgroundColor (Color.White);
		}

		public void hidePlaceInfo()
		{
			mainLayout.RemoveView (scrollPlaces);
			//placesInfoLayout.RemoveAllViews ();
			//placesInfoLayout.SetBackgroundColor (Color.Transparent);
		}

		public void iniPlancesList()
		{
			//_currentPlaces.Clear ();
			placeSpace.RemoveAllViews ();
			placesContainer.RemoveAllViews ();

			listPlaces = new ListView (context);
			listPlaces.LayoutParameters = new LinearLayout.LayoutParams (-1, Configuration.getHeight(345));
			listPlaces.Adapter = new PlaceAdapter (context, _currentPlaces);
			listPlaces.DividerHeight = 0;

			/*
			for (int i = 0; i < _currentPlaces.Count; i++) {

				LinearLayout rowPlace = new LinearLayout (context);
				rowPlace.LayoutParameters = new LinearLayout.LayoutParams (-1, -2);
				rowPlace.Orientation = Orientation.Horizontal;
				rowPlace.SetPadding (30, 20, 30, 20);
				rowPlace.SetGravity (GravityFlags.CenterVertical);

				TextView titulo = new TextView (context);
				titulo.Text = _currentPlaces [i].titulo;
				//titulo.SetTextColor (Color.Black);

				//ImageView imgIcon = new ImageView (context);
				//imgIcon.SetImageBitmap (Bitmap.CreateScaledBitmap (getBitmapFromAsset (_currentPlaces[i].pathIcon), Configuration.getWidth (30), Configuration.getWidth (30), true));

				titulo.SetPadding (Configuration.getWidth(48), 0, 0, 0);
				//rowPlace.AddView (imgIcon);
				rowPlace.AddView (titulo);

				if (i % 2 == 0) {
					rowPlace.SetBackgroundColor (Color.ParseColor ("#F0AE11"));
					titulo.SetTextColor (Color.White);
				} else {
					titulo.SetTextColor (Color.ParseColor("#F0AE11"));
				}

				rowPlace.Clickable = true;
				placesContainer.AddView (rowPlace);
				_placesLayout.Add (rowPlace);

			}*/


			placesContainer.AddView (listPlaces);
			placeSpace.AddView(placesContainer);




		}


		public void updatePlaces()
		{
			for (int i = 0; i < _placesLayout.Count; i++) 
			{
				_placesLayout [i].Click += showPopOut;
				mainLayout.AddView (_placesLayout [i]);
			}
		}

		private void showPopOut (object sender, EventArgs e)
		{
			LinearLayout popOutLayout = new LinearLayout (context);
			popOutLayout.LayoutParameters = new LinearLayout.LayoutParams (Configuration.getWidth(500),Configuration.getWidth(500));
			popOutLayout.SetBackgroundColor (Color.White);


			//infoPopUp.ContentView = popOutLayout;
			infoPopUp.ShowAsDropDown(popOutLayout);
		}

	}
}
	