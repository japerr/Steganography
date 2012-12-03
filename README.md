Steganography
=============

Library which allows multiple different carrier streams to be used as one continuous stream

### Usage

    ICarrierStreamFactory factory = new CarrierStreamFactory();
    factory.RegisterCarrierStreams();

    IList<Stream> carrierFiles = new List<Stream>
    {
    	File.Open("1_Carrier_File", FileMode.Open),
    	File.Open("2_Carrier_File", FileMode.Open),
    	File.Open("3_Carrier_File", FileMode.Open),
    	File.Open("4_Carrier_File", FileMode.Open),
    	File.Open("5_Carrier_File", FileMode.Open)
    };
		
    using (Stream carrierClusterStream = factory.BuildClusterStream<CarrierClusterStream>(
    	new OneKeySequence(), carrierFiles))
    {
    	//.... Use the new carrierClusterStream like a regular stream
    }

### Documentation

	Run: build-documentation.bat
	
### Code Coverage Reports

	Run: Tests\CodeCoverage\build-reports.bat
	
	Then open anyone of the html reports located in the CoverageReports folder.