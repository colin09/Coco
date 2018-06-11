using System;
using Microsoft.ML;
using Microsoft.ML.Runtime.Api;
using Microsoft.ML.Trainers;
using Microsoft.ML.Transforms;

//预测鸢尾花的类型
namespace ml.S1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("start.....");
            // STEP 2: Create a pipeline and load your data
            var pipeline = new LearningPipeline();
            var dataPath = @"data/iris.data";
            
            pipeline.Add(new TextLoader<IrisData>(dataPath,separator:","));

            // STEP 3: Transform your data
            // Assign numeric values to text in the "Label" column, because only numbers can be processed during model training
            pipeline.Add(new Dictionarizer("Label"));
            // Puts all features into a vector
            pipeline.Add(new ColumnConcatenator("Features", "SepalLength", "SepalWidth", "PetalLength", "PetalWidth"));

            // STEP 4: Add learner
            // Add a learning algorithm to the pipeline. 
            // This is a classification scenario (What type of iris is this?)
            pipeline.Add(new StochasticDualCoordinateAscentClassifier());

            // Convert the Label back into original text (after converting to number in step 3)
            pipeline.Add(new PredictedLabelColumnOriginalValueConverter(){PredictedLabelColumn = "PredictedLabel"});

            // STEP 5: Train your model based on the data set
            var model =  pipeline.Train<IrisData,IrisPrediction>();

            // STEP 6: Use your model to make a prediction
            // You can change these numbers to test different predictions
            var prediction = model.Predict(new IrisData(){
                SepalLength = 3.3f,
                SepalWidth = 1.3f,
                PetalLength = 0.2f,
                PetalWidth = 5.1f,
            });

            System.Console.WriteLine($"Predicted flower type is {prediction.PredictedLabels}");
        }
    }



    // STEP 1: Define your data structures

    // IrisData is used to provide training data, and as  input for prediction operations
    // - First 4 properties are inputs/features used to predict the label
    // - Label is what you are predicting, and is only set when training
    class IrisData{
        [Column("0")]
        public float SepalLength; //花瓣长度
        [Column("1")]
        public float SepalWidth;
        [Column("2")]
        public float PetalLength; //萼片长度
        [Column("3")]
        public float PetalWidth;
        [Column("4")]
        [ColumnName("Label")]
        public string Label;
    }

    // IrisPrediction is the result returned from prediction operations
    class IrisPrediction{
        [ColumnName("PredictedLabel")]
        public string PredictedLabels;
    }
}
