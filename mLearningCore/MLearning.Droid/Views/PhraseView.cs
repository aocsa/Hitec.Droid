
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

namespace MLearning.Droid
{
	public class PhraseView : RelativeLayout
	{
		TextView txtPhrase;
		TextView txtAuthor;
		ImageView imgComilla;
		ImageView imgBarra;
		Context context;

		LinearLayout linearBarra;
		LinearLayout linearTextContainer;
		LinearLayout linearAll;

		int Altura =0;

		public PhraseView (Context context) :
		base (context)
		{
			this.context = context;
			Initialize ();
		}


		void Initialize ()
		{

			this.LayoutParameters = new RelativeLayout.LayoutParams (-1,-2);
			this.SetGravity (GravityFlags.Center);

			linearAll = new LinearLayout (context);
			linearTextContainer= new LinearLayout (context);
			linearBarra = new LinearLayout (context);

			txtAuthor = new TextView (context);
			txtPhrase = new TextView (context);
			imgBarra = new ImageView (context);
			imgComilla = new ImageView (context);

			linearAll.LayoutParameters = new LinearLayout.LayoutParams (Configuration.getWidth (582), -2);
			linearTextContainer.LayoutParameters = new LinearLayout.LayoutParams (Configuration.getWidth(552),-2);
			linearBarra.LayoutParameters = new LinearLayout.LayoutParams (Configuration.getWidth(30),-2);

			linearAll.Orientation = Orientation.Horizontal;
			linearBarra.Orientation = Orientation.Vertical;
			linearTextContainer.Orientation = Orientation.Vertical;

			linearAll.SetGravity (GravityFlags.Center);
			//linearBarra.SetGravity (GravityFlags.CenterHorizontal);
			linearTextContainer.SetGravity (GravityFlags.CenterVertical);

			//txtPhrase.SetTextSize (ComplexUnitType.Px, Configuration.getHeight (40));
			//txtAuthor.SetTextSize(ComplexUnitType.Px, Configuration.getHeight (30));

			txtPhrase.SetTextSize (ComplexUnitType.Dip, 21.0f);
			txtAuthor.SetTextSize(ComplexUnitType.Dip, 16.0f);

			txtAuthor.SetTextColor (Color.ParseColor("#b0afb5"));

			linearBarra.AddView (imgComilla);
			//linearBarra.AddView (imgBarra);
			linearTextContainer.AddView (txtPhrase);
			linearTextContainer.AddView (txtAuthor);

			linearAll.AddView (linearBarra);
			linearAll.AddView (linearTextContainer);

			this.AddView (linearAll);

		}

		private String _phrase;
		public String Phrase{
			get{ return _phrase;}
			set{ _phrase = value;
				txtPhrase.TextFormatted = Html.FromHtml (_phrase);
				//txtPhrase.Text = _phrase;
			}

		}

		private String _author;
		public String Author{
			get{ return _author;}
			set{ _author = value;
				txtAuthor.Text = _author;
				//Altura = linearTextContainer.LayoutParameters.Height;
			}

		}

		private String _imagenComilla;
		public String ImagenComilla{
			get{ return _imagenComilla;}
			set{ _imagenComilla = value;

				imgComilla.SetImageBitmap(Bitmap.CreateScaledBitmap (getBitmapFromAsset (_imagenComilla), Configuration.getWidth( 30), Configuration.getHeight (30), true));
			}
		}

		private String _imagenBarra;
		public String ImagenBarra{
			get{ return _imagenBarra;}
			set{ _imagenBarra = value;
				//	int valor = Altura;// - Configuration.getHeight (30);
				//	imgBarra.SetImageBitmap(Bitmap.CreateScaledBitmap (getBitmapFromAsset (_imagenBarra), Configuration.getWidth( 10),100, true));
			}
		}



		private String _colortext;
		public String ColorText{
			get{ return _colortext;}
			set{ _colortext = value;
				txtPhrase.SetTextColor(Color.ParseColor(_colortext));	}

		}


		public Bitmap getBitmapFromAsset( String filePath) {
			System.IO.Stream s =context.Assets.Open (filePath);
			Bitmap bitmap = BitmapFactory.DecodeStream (s);

			return bitmap;
		}


	}
}

