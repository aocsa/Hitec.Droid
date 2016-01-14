using System;
using System.Collections.Generic;

namespace MLearning.Droid
{
	public class AddResources
	{

		public List<String> addList = new List<String>(); 
		 
		public List<String> bannerList = new List<String>(); 

		public AddResources ()
		{

			addList.Add ("https://dl.dropboxusercontent.com/u/8925441/banners/v1.png");
			addList.Add ("https://dl.dropboxusercontent.com/u/8925441/banners/v2.png");
			addList.Add ("https://dl.dropboxusercontent.com/u/8925441/banners/v3.png");
			addList.Add ("https://dl.dropboxusercontent.com/u/8925441/banners/v4.png");
			addList.Add ("https://dl.dropboxusercontent.com/u/8925441/banners/v5.png");

			addList.Add ("https://dl.dropboxusercontent.com/u/8925441/banners/w1.png");
			addList.Add ("https://dl.dropboxusercontent.com/u/8925441/banners/w2.png");
			addList.Add ("https://dl.dropboxusercontent.com/u/8925441/banners/w3.png");

			bannerList.Add ("https://dl.dropboxusercontent.com/u/8925441/banners/banner-01.png");
			bannerList.Add ("https://dl.dropboxusercontent.com/u/8925441/banners/banner-02.png");
			bannerList.Add ("https://dl.dropboxusercontent.com/u/8925441/banners/banner-03.png");


			//ImageView imgProfile = new ImageView (context);

			//Picasso.With(context).Load(item.im_profile).Resize(Configuration.getWidth(45),Configuration.getWidth(45)).CenterCrop().Into(imgProfile);


		}

		private static AddResources instance;
		 

		public static AddResources Instance
		{
			get 
			{
				if (instance == null)
				{
					instance = new AddResources();
				}
				return instance;
			}
		}

	}
}

