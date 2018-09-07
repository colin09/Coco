using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.ML;
using Microsoft.ML.Models;
using Microsoft.ML.Runtime.Api;
using Microsoft.ML.Trainers;
using Microsoft.ML.Transforms;

namespace ml.S2
{
    class Program
    {
        const string dataPath = @".\data\taxi-fare-train.csv";
        const string testDataPath = @".\data\taxi-fare-test.csv";
        const string modelPath = @".\Models\Model.zip";
        const string modelDirectory = @".\Models";



        static async Task<PredictionModel<TaxiTrip, TaxiTripFarePrediction>> Train()
        {
            var pipeline = new LearningPipeline();

            pipeline.Add(new TextLoader<TaxiTrip>(dataPath,useHeader:true,separator:","));
            pipeline.Add(new ColumnCopier(("fare_amount","Label")));
            pipeline.Add(new CategoricalOneHotVectorizer("vendor_id","rate_code","payment_type"));
            pipeline.Add(new ColumnConcatenator("Features",
                                                "vendor_id",
                                                "rate_code",
                                                "passenger_count",
                                                "trip_distance",
                                                "payment_type"));
            pipeline.Add(new FastTreeRegressor());
            PredictionModel<TaxiTrip,TaxiTripFarePrediction> model = pipeline.Train<TaxiTrip,TaxiTripFarePrediction>();
            if(!Directory.Exists(modelDirectory)){
                Directory.CreateDirectory(modelDirectory);
            }
            await model.WriteAsync(modelPath);
            return model;
        }

        static void Evaluate(PredictionModel<TaxiTrip,TaxiTripFarePrediction> model){
            var testData = new TextLoader<TaxiTrip>(testDataPath,useHeader:true,separator:",");
            var evaluator = new RegressionEvaluator();
            RegressionMetrics metrics = evaluator.Evaluate(model,testData);

            System.Console.WriteLine($"Rms = {metrics.Rms}");
            System.Console.WriteLine($"RSquared = {metrics.RSquared}");
        }


        static async Task Main(string[] args)
        {
            Console.WriteLine("start.....");

            PredictionModel<TaxiTrip,TaxiTripFarePrediction> model = await Train();
            Evaluate(model);

            var prediction = model.Predict(TestTrips.Trip1);

            System.Console.WriteLine($"Predicted fare: {prediction.fare_amount}");

        }
    }




    public class TaxiTrip
    {
        [Column(ordinal: "0")]
        public string vendor_id;
        [Column(ordinal: "1")]
        public string rate_code;
        [Column(ordinal: "2")]
        public float passenger_count;
        [Column(ordinal: "3")]
        public float trip_time_in_secs;
        [Column(ordinal: "4")]
        public float trip_distance;
        [Column(ordinal: "5")]
        public string payment_type;
        [Column(ordinal: "6")]
        public float fare_amount;
    }

    public class TaxiTripFarePrediction
    {
        [ColumnName("Score")]
        public float fare_amount;
    }

    static class TestTrips
    {
        internal static readonly TaxiTrip Trip1 = new TaxiTrip
        {
            vendor_id = "VTS",
            rate_code = "1",
            passenger_count = 1,
            trip_distance = 10.33f,
            payment_type = "CSH",
            fare_amount = 0 // predict it. actual = 29.5
        };
    }

    
}
