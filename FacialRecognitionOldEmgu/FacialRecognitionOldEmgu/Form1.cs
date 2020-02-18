using Emgu.CV;
using Emgu.CV.Face;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace FacialRecognitionOldEmgu
{
    public partial class Form1 : Form
    {

        private Capture capture;
        private bool stream;
        private CascadeClassifier cascadeClassifier;
        private CascadeClassifier cascadeClassifierProfile;
        private CascadeClassifier cascadeClassifierEyes;
        private CascadeClassifier cascadeClassifierNose;
        private CascadeClassifier cascadeClassifierMouth;
        private const int MAX_RECORDED_FACES = 500;
        private String recordPath = Application.StartupPath + "/recordedFaces.txt";
        private String imageSavepath = Application.StartupPath + "/Faces";
        private int nbRecordedFaces;

        private Image<Gray, byte>[] trainingImages;
        private int[] labels;
        private bool detectAUX;

        private Face[] recordedFaces;

        private String recognizerFilePath = Application.StartupPath + "/recognizerFilePath.txt";


        private struct Face
        {
            public String path;
            public String name;

        }




        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            stream = false;
            capture = new Capture();
            recordedFaces = new Face[MAX_RECORDED_FACES];
            trainingImages = new Image<Gray, byte>[MAX_RECORDED_FACES];
            labels = new int[MAX_RECORDED_FACES];
            detectAUX = false;



            nbRecordedFaces = 0;

            Console.WriteLine("NbRecorderFaces is " + nbRecordedFaces);

            if (File.Exists(recordPath))
            {
                // Console.WriteLine("NbRecorderFaces is " + nbRecordedFaces);

                String line;

                System.IO.StreamReader file = new System.IO.StreamReader(recordPath);


                line = file.ReadLine();

                String[] parsedLine = new String[2];
                while (line != null)
                {
                    //Console.WriteLine("NbRecorderFaces is " + nbRecordedFaces);

                    parsedLine = line.Split(new char[] { ' ' }, 2);
                    recordedFaces[nbRecordedFaces].path = parsedLine[0];
                    recordedFaces[nbRecordedFaces].name = parsedLine[1];

                    Console.WriteLine("Face path = " + recordedFaces[nbRecordedFaces].path + " name = " + recordedFaces[nbRecordedFaces].name);

                    //trainingImages.Add( new Image<Gray, Byte>(recordedFaces[nbRecordedFaces].path));
                    //labels.Add(nbRecordedFaces);

                    trainingImages[nbRecordedFaces] =  new Image<Gray, byte>(recordedFaces[nbRecordedFaces].path);
                    labels[nbRecordedFaces] = nbRecordedFaces  + 1;

                    nbRecordedFaces++;



                    line = file.ReadLine();


                }


            }
            else
            {
                File.Create(recordPath);
            }


            try
            {
                cascadeClassifier = new CascadeClassifier(Application.StartupPath + "/haarcascade_frontalface_default.xml");
                cascadeClassifierProfile = new CascadeClassifier(Application.StartupPath + "/haarcascade_profileface.xml");
                cascadeClassifierEyes = new CascadeClassifier(Application.StartupPath + "/haarcascade_eye.xml");
                cascadeClassifierNose = new CascadeClassifier(Application.StartupPath + "/Nariz.xml");
                cascadeClassifierMouth = new CascadeClassifier(Application.StartupPath + "/Mouth.xml");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occured: " + ex);
            }
        }

        private void btnStream_Click(object sender, EventArgs e)
        {
            stream = !stream;

            if (stream)
            {
                Application.Idle += videoStream;
                btnStream.Text = "Stop Streaming";
            }
            else
            {
                Application.Idle -= videoStream;
                btnStream.Text = "Start Streaming";
                imgCamUser.Image = null;

            }

           
        }


        private void videoStream(object sender, EventArgs e)
        {


            using (var imageFrame = capture.QueryFrame().ToImage<Bgr, Byte>())
            {
                if (imageFrame != null)
                {
                    var greyFrame = imageFrame.Convert<Gray, byte>();
                    var frontalFaces = cascadeClassifier.DetectMultiScale(greyFrame, 1.1, 10, Size.Empty);
                    foreach (var face in frontalFaces)
                    {
                        imageFrame.Draw(face, new Bgr(Color.Green), 2);

                        MCvTermCriteria mcvCrit = new MCvTermCriteria(30, 0.001);

                        EigenFaceRecognizer recognizer = new EigenFaceRecognizer(200, double.PositiveInfinity);

                        Image<Gray, byte>[] copyTrainingImages = new Image<Gray, byte>[nbRecordedFaces];
                        int[] copyLabels = new int[nbRecordedFaces];

                        for(int i = 0; i < nbRecordedFaces; i++)
                        {
                            copyTrainingImages[i] = trainingImages[i];
                            copyLabels[i] = labels[i];
                        }
                        if (nbRecordedFaces > 0)
                        {


                            recognizer.Train(copyTrainingImages, copyLabels);
                       
                            var grayFace = imageFrame.Copy(face).Resize(64, 64, Emgu.CV.CvEnum.Inter.Cubic).Convert<Gray, Byte>();
                           
                            EigenFaceRecognizer.PredictionResult result = recognizer.Predict(grayFace);

                            String name;

                            if (result.Label == 0)
                                name = "????";
                            else name = recordedFaces[result.Label - 1].name;

                            CvInvoke.PutText(imageFrame, name, new Point(face.Location.X + 10, face.Location.Y - 10),
                                Emgu.CV.CvEnum.FontFace.HersheyComplex, 0.6, new Bgr(0, 255, 0).MCvScalar);

                        
                        }


                    }

                    var profileFaces = cascadeClassifierProfile.DetectMultiScale(greyFrame, 1.1, 10, Size.Empty);
                    foreach (var face in profileFaces)
                    {
                        imageFrame.Draw(face, new Bgr(Color.Yellow), 2);
                    }

                    if (detectAUX)
                    {
                        var eyes = cascadeClassifierEyes.DetectMultiScale(greyFrame, 1.2, 10, Size.Empty);
                        foreach (var eye in eyes)
                        {
                            imageFrame.Draw(eye, new Bgr(Color.Red), 2);
                        }

                        var noses = cascadeClassifierNose.DetectMultiScale(greyFrame, 1.2, 10, Size.Empty);
                        foreach (var nose in noses)
                        {
                            imageFrame.Draw(nose, new Bgr(Color.Black), 2);
                        }

                        var mouths = cascadeClassifierMouth.DetectMultiScale(greyFrame, 1.2, 10, Size.Empty);
                        foreach (var mouth in mouths)
                        {
                            imageFrame.Draw(mouth, new Bgr(Color.Purple), 2);
                        }
                    }


                }

                imgCamUser.Image = imageFrame;
            }


        }


        private void btnSaveFace_Click(object sender, EventArgs e)
        {
            var imageFrame = capture.QueryFrame().ToImage<Bgr, Byte>();

            // Bitmap bitmapFace =imageFrame.ToBitmap();
            //bitmapFace.Save(recordPath);


            var frontalFaces = cascadeClassifier.DetectMultiScale(imageFrame, 1.1, 10, Size.Empty);
            if (frontalFaces.GetLength(0) != 1)
            {
                MessageBox.Show("Please make sure that ONLY one face is detected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (nameBox.Text.Equals(""))
            {
                MessageBox.Show("Please insert a name for the face", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                String facePath = imageSavepath + "/face" + nbRecordedFaces + ".bmp";

                var imgFace = imageFrame.Copy(frontalFaces[0]).Resize(64, 64, Emgu.CV.CvEnum.Inter.Cubic);

                // var bitmapFace = new Bitmap(frontalFaces[0].Width, frontalFaces[0].Height);
                // ImageConverter converter = new ImageConverter();
                //   byte[] faces = (byte[])converter.ConvertTo(bitmapFace, typeof(byte[]));
                Bitmap bitmapFace = imgFace.ToBitmap();
                bitmapFace.Save(facePath);

                using (StreamWriter sw = File.AppendText(recordPath))
                {
                    sw.Write(facePath + " " + nameBox.Text + "\n");
                }

                recordedFaces[nbRecordedFaces].path = facePath;
                recordedFaces[nbRecordedFaces].name = nameBox.Text;

                trainingImages[nbRecordedFaces] = new Image<Gray, byte>(facePath);
                labels[nbRecordedFaces] = nbRecordedFaces + 1;

                

                nbRecordedFaces++;

                MessageBox.Show("Face " + nameBox.Text + " has been recorded");
            }

        }

        private void btnDetect_Click(object sender, EventArgs e)
        {
            detectAUX = !detectAUX;
        }
    }
}
