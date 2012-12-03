Steganography
=============

Library which allows multiple different carrier streams to be used as one continuous stream

### Usage

    ICarrierStreamFactory factory = new CarrierStreamFactory();
    factory.RegisterCarrierStreams();

    using (Stream carrierClusterStream = factory.BuildClusterStream<CarrierClusterStream>(
    	new OneKeySequence(), new List<Stream>{ File.Open("A_Carrier_File", FileMode.Open) }))
    {
    	//.... Use the new carrierClusterStream like a regular stream
    }

### Documentation

	Run: build-documentation.bat
	
### Code Coverage Reports

	Run: Tests\CodeCoverage\build-reports.bat