
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
	public class CustomerImageView : RelativeLayout
	{
		Context context;
		RelativeLayout image;
		LinearLayout background;
		TextView txtDescription;
		TextView txtTitle;
		ImageView imBack;

		LinearLayout relTemp;

		public CustomerImageView (Context context) :
		base (context)
		{
			this.context = context;
			Initialize ();
		}


		void Initialize ()
		{
			this.LayoutParameters = new RelativeLayout.LayoutParams(-1,Configuration.getHeight (412));// LinearLayout.LayoutParams (Configuration.getWidth (582), Configuration.getHeight (394));
			this.SetGravity(GravityFlags.CenterHorizontal);


			imBack = new ImageView (context);
			image = new RelativeLayout(context);
			txtDescription = new TextView (context);
			txtTitle = new TextView (context);
			background = new LinearLayout (context);
			relTemp = new LinearLayout(context);


			//LinearLayout.LayoutParams paramL = new new LinearLayout.LayoutParams (Configuration.getWidth (530), Configuration.getHeight (356));

			background.LayoutParameters = new LinearLayout.LayoutParams (Configuration.getWidth (530), Configuration.getHeight (356));
			background.Orientation = Orientation.Vertical;



			background.SetBackgroundColor (Color.ParseColor ("#50000000"));
			background.BaselineAligned = true;

			image.LayoutParameters = new RelativeLayout.LayoutParams (Configuration.getWidth (582), Configuration.getHeight (394));
			image.SetGravity (GravityFlags.Center);

			relTemp.LayoutParameters = new LinearLayout.LayoutParams (Configuration.getWidth (582), Configuration.getHeight (394));
			relTemp.SetGravity (GravityFlags.Center);

			//RelativeLayout.LayoutParams param = new RelativeLayout.LayoutParams(Configuration.getWidth (530), Configuration.getHeight (356));

			//param.AddRule (LayoutRules.CenterInParent);

			relTemp.AddView (background);


			txtTitle.SetTextColor (Color.ParseColor("#ffffff"));
			txtDescription.SetTextColor(Color.ParseColor("#ffffff"));
			//txtTitle.SetTextSize (ComplexUnitType.Px, Configuration.getHeight (40));
			//txtDescription.SetTextSize (ComplexUnitType.Px, Configuration.getHeight (30));

			txtTitle.SetTextSize (ComplexUnitType.Dip, 21.0f);
			txtDescription.SetTextSize (ComplexUnitType.Dip, 15.0f);
			txtDescription.Ellipsize = Android.Text.TextUtils.TruncateAt.End;
			txtDescription.SetSingleLine (false);
			txtDescription.SetMaxLines (9);
			//txtDescription.line


			background.AddView (txtTitle);
			background.AddView (txtDescription);




			image.AddView (imBack);
			image.AddView (relTemp);

			this.AddView (image);
			//this.AddView (background);


		}

		private String _title;
		public String Title{
			get{ return _title;}
			set{ _title = value;
				txtTitle.Text = _title;
			}

		}

		private String _description;
		public String Description{
			get{ return _description;}
			set{ _description = value;
				txtDescription.Text = _description;
			}

		}

		private String _imagen;
		public String Imagen{
			get{ return _imagen;}
			set{ _imagen = value;
				//Bitmap bm = Configuration.GetImageBitmapFromUrl (_imagen);
				//Drawable dr = new BitmapDrawable (Bitmap.CreateScaledBitmap (bm, Configuration.getWidth (582), Configuration.getHeight (394), true));

				//image.SetBackgroundDrawable (dr);


				Picasso.With (context).Load (_imagen).Resize(Configuration.getWidth(582),Configuration.getHeight(394)).CenterCrop().Into (imBack);



			}


		}


		private Bitmap _imageBitmap;
		public Bitmap ImageBitmap{
			get{ return _imageBitmap;}
			set{ _imageBitmap = value;			

				Drawable dr = new BitmapDrawable (Bitmap.CreateScaledBitmap (_imageBitmap, Configuration.getWidth (582), Configuration.getHeight (394), true));
				image.SetBackgroundDrawable (dr);
			}

		}

		public Bitmap getBitmapFromAsset( String filePath) {
			System.IO.Stream s =context.Assets.Open (filePath);
			Bitmap bitmap = BitmapFactory.DecodeStream (s);

			return bitmap;
		}


	}
}

