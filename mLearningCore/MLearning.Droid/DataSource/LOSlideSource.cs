using System;
using System.ComponentModel;
using Android.Graphics;
using System.Collections.ObjectModel;
using MLearning.Droid;
using Android.Views;
using Android.Content;
using Android.Widget;

namespace DataSource
{
	public class LOSlideSource : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		Context context;

		public LOSlideSource(Context context){
			this.context = context;
		}

		private int _type;

		public int Type
		{
			get { return _type; }
			set
			{
				_type = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Type"));
			}
		}

		/*
		private LOSlideStyle _style;

		public LOSlideStyle Style
		{
			get { return _style; }
			set { _style = value; }
		}*/

		private Color _color;
		public Color ColorTema
		{
			get { return _color; }
			set { _color = value; }
		}


		private string _colorS;
		public string ColorS{
			get{return _colorS; }
			set{_colorS = value; }
		}

		private string _title;

		public string Title
		{
			get { return _title; }
			set
			{
				_title = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Title"));
			}
		}

		private string _author;

		public string Author
		{
			get { return _author; }
			set
			{
				_author = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Author"));
			}
		}


		private string _paragraph;

		public string Paragraph
		{
			get { return _paragraph; }
			set
			{
				_paragraph = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Paragraph"));
			}
		}

		private Bitmap _image;

		public Bitmap Image
		{
			get { return _image; }
			set
			{
				_image = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Image"));
			}
		}


		private byte[] _imagebytes;

		public byte[] ImageBytes
		{
			get { return _imagebytes; }
			set
			{
				_imagebytes = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ImageBytes"));
			}
		}



		private string _imageurl;

		public string ImageUrl
		{
			get { return _imageurl; }
			set { _imageurl = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ImageUrl"));
			}
		}


		private string _videourl;

		public string VideoUrl
		{
			get { return _videourl; }
			set
			{
				_videourl = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("VideoUrl"));
			}
		}


		private ObservableCollection<LOItemSource> _itemize;

		public ObservableCollection<LOItemSource> Itemize
		{
			get { return _itemize; }
			set
			{
				_itemize = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Itemize"));
			}
		}


		public RelativeLayout getViewSlide(){

			if (_type == 1) {
				Template1 plantilla = new Template1 (context);
				plantilla.Title = _title;
				plantilla.Author = _author;
				plantilla.Contenido = _paragraph;
				plantilla.ImageUrl = _imageurl;
				plantilla.ColorTexto = _colorS;
				//Console.WriteLine ("CREA PLANTILLAAAAAAAAA  111111");
				return plantilla;

			}
			if (_type == 2) {
				Template2 plantilla = new Template2 (context);
				plantilla.Title = _title;
				plantilla.Contenido = _paragraph;
				plantilla.ColorTexto = _colorS;

				//Console.WriteLine ("CREA PLANTILLAAAAAAAAA  222222");
				return plantilla;

			}
			if (_type == 3) {
				Template3 plantilla = new Template3 (context);
				plantilla.Title = _title;
				string [] lista = new string[_itemize.Count];
				for (int i = 0; i < _itemize.Count; i++) {
					lista[i]=_itemize[i].Text;
				}
				//string[] lista = {"sdfsdf sdfs fsdf sf sdfs"," dfsdfsdf sdfsd fsd ds"," fsdf sfsdf sdfsd"," fdsfsdf sdfsdf sdfsf"};
				//Console.WriteLine ("CREA PLANTILLAAAAAAAAA  333333");
				plantilla.ListItems = lista;
				return plantilla;
			}
			if (_type == 4) {
				Template4 plantilla = new Template4 (context);
				return plantilla;
				//Console.WriteLine ("CREA PLANTILLAAAAAAAAA  4444444444");
			}
			if (_type == 5) {
				PhraseView plantilla = new PhraseView (context);
				plantilla.Author = _author;
				plantilla.Phrase = _paragraph;
				plantilla.ImagenComilla = "icons/comillasa.png";
				plantilla.ImagenBarra = "icons/lineafraseamarilla.png";
				//Console.WriteLine ("CREA PLANTILLAAAAAAAAA  5");
				return plantilla;

			}
			if (_type == 6) {
				CustomerImageView plantilla = new CustomerImageView (context);
				plantilla.Title = _title;
				plantilla.Description = _paragraph;
				plantilla.Imagen = _imageurl;//BitmapFactory.DecodeByteArray (_imagebytes, 0, _imagebytes.Length);
				plantilla.ColorTexto = _colorS;
				return plantilla;
			}

			if (_type == 7) {
				CustomerVideoView plantilla = new CustomerVideoView (context);
				plantilla.Title = _title;
				plantilla.Imagen = _imageurl;
				plantilla.ImagenPlay = "images/playa.png";
				return plantilla;
			}
			return null;
		}

	}
}

