# DeviceDataApi
Coding Test Project

I attempted to solve the problem in 2 ways (each has it's own pros and cons). Overall problem solution is done via
tranforming data from two different device types (json outputs) into some standard data type and then agreggation of data to display it. 
Storage implemented via distributed cache. Technically, in case of using relational database aggregation logic could be done in stored procedure.

Solution is a .NET 5 API that consists of the following endpoints:
* POST /api/v1/device-data (Option 1)
* POST /api/v1/device-type-a-data (Option 2)
* POST /api/v1/device-type-b-data (Option 2)
* GET /api/v1/device-data
* DELETE /api/v1/device-data

Assuming that we have two different types of measuring IoT devices that are calling the API to record their mesurements and we want to display data on some dashboard.

**Option 1** - one generic endpoint that will accept differently structured JSONs and then we try to deserialize it into know types.

Pros of this approach is that all our devices could have the same firmware of the api client.

**Option 2** - enpoint per device type. Each device is calling into specific endpoint (_/api/v1/device-type-a-data_ or _device-type-b-data_) to report their measurements.

Pros of this approach is having strongly typed inputs.
Cons of this approach, that each device type should have different api client firmware piece (could be solved though by adding configuration endpoint that will accept device type
as input and return api endpoint to call).

Both apporoaches will require code changes on adding new device that will have different input.

Solution has [Postman](https://github.com/exbarboss/DeviceDataApi/tree/master/PostmanTests) integration tests implemented. 
Import both collection and environment files into postman, start API (update environemtn URL variable if needed), add collection to the Runner and run it.
