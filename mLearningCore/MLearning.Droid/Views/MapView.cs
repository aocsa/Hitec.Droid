
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
		public ScaleImageView mapImage;
		public List<LinearLayout> _placesLayout = new List<LinearLayout>();
		public List<MapItemInfo> _placesData = new List<MapItemInfo> ();
		PopupWindow infoPopUp; 
		LinearLayout fullInfo;

		List<PlaceItem> _currentPlaces = new List<PlaceItem>();
		ListView  listPlaces;

		public LinearLayout placesInfoLayout;
		public LinearLayout placesContainer;

		int widthInDp;
		int heightInDp;

		Context context;

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

		public void ini(){

			placesInfoLayout = new LinearLayout (context);
			placesInfoLayout.LayoutParameters = new LinearLayout.LayoutParams (-1, -1);
			placesInfoLayout.SetPadding(30,30,30,30);

			mainLayout = new RelativeLayout (context);
			mainLayout.LayoutParameters = new RelativeLayout.LayoutParams (-1,-1);

			mapImage = new ScaleImageView (context,null);
			//mapImage.SetMinimumWidth (Configuration.WIDTH_PIXEL);
			//mapImage.LayoutParameters = new LinearLayout.LayoutParams (-1, Configuration.getHeight (500));

			//mapImage.SetScaleType (ImageView.ScaleType.CenterInside);
			//mapImage.ZoomTo(2,0,0);


			mapSpace = new LinearLayout (context);
			mapSpace.LayoutParameters = new LinearLayout.LayoutParams (-1, Configuration.getHeight (800));
			mapSpace.AddView (mapImage);



			placeSpace = new VerticalScrollView (context);
			placeSpace.LayoutParameters = new LinearLayout.LayoutParams (-1, Configuration.getHeight(200));
			placeSpace.SetY (Configuration.getHeight (800));
			placeSpace.SetBackgroundColor (Color.White);

			//mainLayout.AddView (mapSpace);
			//mainLayout.AddView (placeSpace);

			PlaceItem p1 = new PlaceItem{ titulo = "Piscacucho-Wayllabamba", detalle = "........." };
			PlaceItem p2 = new PlaceItem{ titulo = "La arquitectura Inca", detalle = "........." };
			PlaceItem p3 = new PlaceItem{ titulo = "Wayllabamba-Pacaymayo", detalle = "........." };
			PlaceItem p4 = new PlaceItem{ titulo = "Llaqtapata", detalle = "........." };
			PlaceItem p5 = new PlaceItem{ titulo = "Miskay", detalle = "........." };
			PlaceItem p6 = new PlaceItem{ titulo = "Puesto de control Piscacucho (km 82)", detalle = "........." };
			PlaceItem p7 = new PlaceItem{ titulo = "Campamento Wayllabamba", detalle = "........." };
			PlaceItem p8 = new PlaceItem{ titulo = "Place", detalle = "........." };

			_currentPlaces.Add (p1);
			_currentPlaces.Add (p2);
			_currentPlaces.Add (p3);
			_currentPlaces.Add (p4);
			_currentPlaces.Add (p4);
			_currentPlaces.Add (p5);
			_currentPlaces.Add (p6);
			_currentPlaces.Add (p7);


			//listPlaces = new ListView (context);
			//listPlaces.Adapter = new PlaceAdapter(context, _currentPlaces);
			//listPlaces.ItemClick+= ListCursos_ItemClick;


			//placeSpace.AddView (listPlaces);
			mainLayout.AddView (mapSpace);
			mainLayout.AddView (placeSpace);
			mainLayout.AddView (placesInfoLayout);
			iniPlancesList ();

		}

		public void showPLaceInfo(object sender, EventArgs e)
		{

			placeInfoOpen = true;
			placesInfoLayout.RemoveAllViews ();
			TextView detalle = new TextView (context);
			detalle.Text  = "Tras cruzar un puente sobre el río Urubamba usted habrá iniciado oficialmente su aventura rumbo a la ciudadela perdida de los incas. Las primeras dos horas de caminata se realizan sobre un terreno plano, en un típico valle húmedo interandino, rodeado de matas de chilca, papas silvestres, cactus, tara y coloridas flores de retama. ";

			ImageView image1 = new ImageView (context);
			image1.SetImageBitmap (getBitmapFromAsset ("images/fondounidad.png"));


			placesInfoLayout.SetBackgroundColor (Color.White);
			placesInfoLayout.AddView (detalle);
			placesInfoLayout.AddView (image1);



		}

		public void hidePlaceInfo()
		{
			placesInfoLayout.RemoveAllViews ();
			placesInfoLayout.SetBackgroundColor (Color.Transparent);
		}

		public void iniPlancesList()
		{

			placesContainer = new LinearLayout (context);
			placesContainer.LayoutParameters = new LinearLayout.LayoutParams (-1, Configuration.getHeight(500));
			placesContainer.Orientation = Orientation.Vertical;

			for (int i = 0; i < _currentPlaces.Count; i++) {

				LinearLayout rowPlace = new LinearLayout (context);
				rowPlace.LayoutParameters = new LinearLayout.LayoutParams (-1, -2);

				TextView titulo = new TextView (context);
				titulo.Text = _currentPlaces [i].titulo;

				rowPlace.AddView (titulo);
				placesContainer.AddView (rowPlace);

			}

			placeSpace.AddView (placesContainer);

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
	