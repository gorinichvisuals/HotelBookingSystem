AuthController Documentation:
Overview
AuthController is an API controller for customer authentication, including creating new customers and logging in existing ones. The base route for this controller is api/auth.

Dependencies
IAuthService: Handles authentication logic.
ILogger<AuthController>: Logs errors and information.
Endpoints
CreateCustomer
HTTP Method: POST
Route: api/auth/create
Description: Creates a new customer and returns an authentication token.

Parameters
customerCreateDTO (body): Customer details.
Request Body
Name (string): Required, valid email address, max length 128.
Email (string): Required, valid email address, max length 128.
Phone (string): Required, valid email address, max length 16.
Password (string): Required.
Responses
201 Created: Returns an AuthTokenDTO.
400 Bad Request: Returns an error message.
429 Too Many Requests: Returns an error message.
500 Internal Server Error: Returns an error message.
Rate Limiting
Enabled for CustomerAuth.

Example Request:
{
  "name": "John Doe",
  "email": "john.doe@example.com",
  "phone": "1234567890",
  "password": "securePassword123"
}

Example Response: 
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}

Login
HTTP Method: POST
Route: api/auth/login
Description: Logs in an existing customer and returns an authentication token.

Parameters
loginDTO (body): Login details.
Request Body
Email (string): Required, valid email address, max length 128.
Password (string): Required.
Responses
200 Ok: Returns an AuthTokenDTO.
400 Bad Request: Returns an error message.
404 Not Found: Returns an error message.
429 Too Many Requests: Returns an error message.
500 Internal Server Error: Returns an error message.
Rate Limiting
Enabled for CustomerAuth.

Example Request:
{
  "email": "john.doe@example.com",
  "password": "securePassword123"
}

Example Response:
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}

Data Transfer Objects (DTOs)
AuthTokenDTO
Represents the authentication token.

Token (string): The authentication token.
CustomerCreateDTO
Represents the details required to create a new customer.

Name (string): Required, valid email address, max length 128.
Email (string): Required, valid email address, max length 128.
Phone (string): Required, valid email address, max length 16.
Password (string): Required.
AuthLoginDTO
Represents the login details of an existing customer.

Email (string): Required, valid email address, max length 128.
Password (string): Required.
Logging
Errors are logged using the ILogger service with the error message and exception details.

Exception Handling
Both actions have try-catch blocks to handle exceptions and log errors. If an exception occurs, a 500 Internal Server Error response is returned with the exception message.

----------------------------------------------------------------

CustomerController Documentation:
Overview
CustomerController is an API controller for managing customer-related operations, including retrieving personal information, updating customer details, and changing passwords. The base route for this controller is api/customers.

Dependencies
ICustomerService: Handles customer-related logic.
ISessionProvider: Provides session-related information.
ILogger<CustomerController>: Logs errors and information.
Authorization
This controller requires authorization and is restricted to users with the Customer role.

Endpoints
GetCustomerPersonalInfo
HTTP Method: GET
Route: api/customers/personal-info
Description: Retrieves the personal information of the currently authenticated customer.

Parameters
None
Responses
200 OK: Returns a CustomerDTO.
401 Unauthorized: Returns an error message.
403 Forbidden: Returns an error message.
404 Not Found: Returns an error message.
500 Internal Server Error: Returns an error message.
Example Response:
{
  "Name": "John Doe",
  "Email": "john.doe@example.com",
  "Phone": "123456789",
  "RegisterAt": "2023-01-01T00:00:00Z",
  "Bookings": [
    {
      "Id": 1,
      "Amount": "100.00",
      "ConfirmedByAdmin": true,
      "IsPaid": true,
      "IsCancelled": false,
      "PaymentType": "Credit Card",
      "CheckInDate": "2024-01-01T00:00:00Z",
      "CheckOutDate": "2024-01-05T00:00:00Z",
      "Room": {
        "Id": 1,
        "RoomNumber": 101,
        "RoomType": "Deluxe",
        "RoomClass": "A",
        "Description": "A luxurious room",
        "Amount": "100.00",
        "IsAvailable": true,
        "Photos": ["photo1.jpg", "photo2.jpg"]
      },
      "Payment": {
        "Id": 1,
        "PaymentId": "pay123",
        "Amount": "100.00",
        "PaymentDate": "2023-01-01T00:00:00Z",
        "PaymentStatus": "Completed"
      }
    }
  ]
}

UpdateCustomer
HTTP Method: PUT
Route: api/customers/update
Description: Updates the personal information of the currently authenticated customer.

Parameters
updateDTO (body): The updated customer information.
Request Body
Name (string): The new name of the customer.
Email (string): The new email address of the customer.
Phone (string): The new phone number of the customer.
Responses
200 OK: Returns the updated CustomerDTO.
401 Unauthorized: Returns an error message.
403 Forbidden: Returns an error message.
404 Not Found: Returns an error message.
500 Internal Server Error: Returns an error message.

Example Request:
{
  "Name": "John Doe",
  "Email": "john.doe@example.com",
  "Phone": "123456789"
}

Example Response:
{
  "Name": "John Doe",
  "Email": "john.doe@example.com",
  "Phone": "123456789",
  "RegisterAt": "2023-01-01T00:00:00Z",
  "Bookings": [
    {
      "Id": 1,
      "Amount": "100.00",
      "ConfirmedByAdmin": true,
      "IsPaid": true,
      "IsCancelled": false,
      "PaymentType": "Credit Card",
      "CheckInDate": "2024-01-01T00:00:00Z",
      "CheckOutDate": "2024-01-05T00:00:00Z",
      "Room": {
        "Id": 1,
        "RoomNumber": 101,
        "RoomType": "Deluxe",
        "RoomClass": "A",
        "Description": "A luxurious room",
        "Amount": "100.00",
        "IsAvailable": true,
        "Photos": ["photo1.jpg", "photo2.jpg"]
      },
      "Payment": {
        "Id": 1,
        "PaymentId": "pay123",
        "Amount": "100.00",
        "PaymentDate": "2023-01-01T00:00:00Z",
        "PaymentStatus": "Completed"
      }
    }
  ]
}

ChangePassword
HTTP Method: PUT
Route: api/customers/change-password
Description: Changes the password of the currently authenticated customer.

Parameters
changePasswordDTO (body): The old and new passwords.
Request Body
OldPassword (string): The current password of the customer.
NewPassword (string): The new password for the customer.
Responses
200 OK: Password change was successful.
401 Unauthorized: Returns an error message.
403 Forbidden: Returns an error message.
404 Not Found: Returns an error message.
500 Internal Server Error: Returns an error message.

Example Request:
{
  "OldPassword": "oldpassword123",
  "NewPassword": "newpassword123"
}

Example Response:
{}

Data Transfer Objects (DTOs)
CustomerDTO
Represents the personal information of a customer.

Name (string): The name of the customer.
Email (string): The email address of the customer.
Phone (string): The phone number of the customer.
RegisterAt (DateTime): The registration date of the customer.
Bookings (ICollection<BookingDTO>): The bookings made by the customer.
BookingDTO
Represents a booking made by a customer.

Id (int): The booking ID.
Amount (string): The amount for the booking.
ConfirmedByAdmin (bool): Whether the booking is confirmed by an admin.
IsPaid (bool): Whether the booking is paid.
IsCancelled (bool): Whether the booking is cancelled.
PaymentType (string): The type of payment for the booking.
CheckInDate (DateTime): The check-in date for the booking.
CheckOutDate (DateTime): The check-out date for the booking.
Room (RoomDTO): The room details for the booking.
Payment (PaymentDTO?): The payment details for the booking.
RoomDTO
Represents a room in the hotel.

Id (int): The room ID.
RoomNumber (int): The room number.
RoomType (string): The type of the room.
RoomClass (string): The class of the room.
Description (string?): The description of the room.
Amount (string): The amount for the room.
IsAvailable (bool): Whether the room is available.
Photos (ICollection<string>?): Photos of the room.
PaymentDTO
Represents a payment made for a booking.

Id (int): The payment ID.
PaymentId (string): The payment ID from the payment gateway.
Amount (string): The amount for the payment.
PaymentDate (DateTime): The date of the payment.
PaymentStatus (string): The status of the payment.
CustomerUpdateDTO
Represents the updated personal information of a customer.

Name (string): The new name of the customer.
Email (string): The new email address of the customer.
Phone (string): The new phone number of the customer.
CustomerChangePasswordDTO
Represents the old and new passwords for changing the password of a customer.

OldPassword (string): The current password of the customer.
NewPassword (string): The new password for the customer.
Logging
Errors are logged using the ILogger service with the error message and exception details.

Exception Handling
The actions have a try-catch block to handle exceptions and log errors. If an exception occurs, a 500 Internal Server Error response is returned with the exception message.

----------------------------------------------------------------

RoomController Documentation:
Overview
RoomController is an API controller for handling room-related operations, including retrieving a list of rooms based on filters. The base route for this controller is api/rooms.

Dependencies
IRoomService: Handles room-related logic.
ILogger<RoomController>: Logs errors and information.
Endpoints
GetRooms
HTTP Method: GET
Route: api/rooms
Description: Retrieves a list of rooms based on the provided filters.

Parameters
filterDTO (query): Contains the filters for retrieving rooms.
cancellationToken (query): Token to monitor for cancellation requests.
Request Query Parameters
SkipItems (int): Number of items to skip. Default is 0.
TakeItems (int): Number of items to take. Default is 10.
Ascending (bool): Sort order. Default is true (ascending).
RoomType (string): Optional room type filter.
RoomClass (string): Optional room class filter.
Floor (int): Optional floor filter.
CheckInDate (DateTime): Optional check-in date filter.
CheckOutDate (DateTime): Optional check-out date filter.
Responses
200 OK: Returns a RoomsDTO.
429 Too Many Requests: Returns an error message.
500 Internal Server Error: Returns an error message.
Rate Limiting
Enabled for GetRooms.

Example Request:
GET /api/rooms?SkipItems=0&TakeItems=10&Ascending=true&RoomType=Deluxe&RoomClass=Suite&Floor=2&CheckInDate=2023-12-01&CheckOutDate=2023-12-05

Example Response:
{
  "Rooms": [
    {
      "Id": 1,
      "RoomNumber": 101,
      "RoomType": "Deluxe",
      "RoomClass": "Suite",
      "Description": "A deluxe suite with a view",
      "Amount": "150.00",
      "IsAvailable": true,
      "Photos": [
        "photo1.jpg",
        "photo2.jpg"
      ]
    }
  ],
  "Count": 1
}

Data Transfer Objects (DTOs)
RoomFilterDTO
Represents the filters for retrieving rooms.

SkipItems (int): Number of items to skip. Default is 0.
TakeItems (int): Number of items to take. Default is 10.
Ascending (bool): Sort order. Default is true (ascending).
RoomType (string): Optional room type filter.
RoomClass (string): Optional room class filter.
Floor (int): Optional floor filter.
CheckInDate (DateTime): Optional check-in date filter.
CheckOutDate (DateTime): Optional check-out date filter.
RoomsDTO
Represents the response containing a list of rooms and the total count.

Rooms (ICollection<RoomDTO>): Collection of RoomDTO.
Count (int): Total number of rooms.
RoomDTO
Represents the details of a room.

Id (int): Room ID.
RoomNumber (int): Room number.
RoomType (string): Room type.
RoomClass (string): Room class.
Description (string): Description of the room.
Amount (string): Cost of the room.
IsAvailable (bool): Availability status.
Photos (ICollection<string>): Collection of room photos (optional).
Logging
Errors are logged using the ILogger service with the error message and exception details.

Exception Handling
The action has a try-catch block to handle exceptions and log errors. If an exception occurs, a 500 Internal Server Error response is returned with the exception message.

----------------------------------------------------------------

PaymentController Documentation:
Overview
PaymentController is an API controller for managing payment operations, including creating invoices for payments. The base route for this controller is api/payments.

Dependencies
IPaymentService: Handles payment-related logic.
ILogger<PaymentController>: Logs errors and information.
Authorization
This controller requires authorization and is restricted to users with the Customer role.

Endpoints
CreateMonoPayment
HTTP Method: POST
Route: api/payments/create-invoice
Description: Creates an invoice for a payment.

Parameters
paymentCreateDTO (body): Details required to create the payment invoice.
Request Body
RoomId (int): ID of the room associated with the payment.
Responses
200 OK: Returns an InvoiceMonoGetDTO.
400 Bad Request: Returns an error message.
401 Unauthorized: Returns an error message.
403 Forbidden: Returns an error message.
404 Not Found: Returns an error message.
429 Too Many Requests: Returns an error message.
500 Internal Server Error: Returns an error message.

Example Request:
{
  "RoomId": 1
}

Example Response: 
{
  "invoiceId": "inv123",
  "pageUrl": "https://paymentgateway.com/invoice/inv123"
}

Data Transfer Objects (DTOs)
InvoiceMonoGetDTO
Represents the details of an invoice generated for a payment.

InvoiceId (string?): ID of the invoice.
PageURL (string?): URL of the invoice page.
PaymentCreateDTO
Represents the details required to create a payment invoice.

RoomId (int): ID of the room associated with the payment.
Logging
Errors are logged using the ILogger service with the error message and exception details.

Exception Handling
The action has a try-catch block to handle exceptions and log errors. If an exception occurs, a 500 Internal Server Error response is returned with the exception message.

----------------------------------------------------------------

BookingController Documentation:
Overview
BookingController is an API controller for managing booking operations, including creating, updating, and canceling bookings. The base route for this controller is api/bookings.

Dependencies
IBookingService: Handles booking-related logic.
ISessionProvider: Provides session information.
ILogger<BookingController>: Logs errors and information.
Authorization
This controller requires authorization and is restricted to users with the Customer role.

Endpoints
CreateBooking
HTTP Method: POST
Route: api/bookings/create
Description: Creates a new booking.

Parameters
bookingCreateDTO (body): Details of the booking to be created.
Request Body
RoomId (int): ID of the room to be booked.
PaymentType (PaymentType): Type of payment.
CheckInDate (DateTime): Check-in date.
CheckOutDate (DateTime): Check-out date.
Responses
201 Created: Returns a BookingDTO.
401 Unauthorized: Returns an error message.
403 Forbidden: Returns an error message.
404 Not Found: Returns an error message.
500 Internal Server Error: Returns an error message.

Example Request:
{
  "RoomId": 1,
  "PaymentType": "CreditCard",
  "CheckInDate": "2023-12-01T00:00:00Z",
  "CheckOutDate": "2023-12-05T00:00:00Z"
}

Example Response:
{
  "Id": 123,
  "Amount": "500.00",
  "ConfirmedByAdmin": false,
  "IsPaid": true,
  "IsCancelled": false,
  "PaymentType": "CreditCard",
  "CheckInDate": "2023-12-01T00:00:00Z",
  "CheckOutDate": "2023-12-05T00:00:00Z",
  "Room": {
    "Id": 1,
    "RoomNumber": 101,
    "RoomType": "Deluxe",
    "RoomClass": "Suite",
    "Description": "A deluxe suite with a view",
    "Amount": "150.00",
    "IsAvailable": true,
    "Photos": [
      "photo1.jpg",
      "photo2.jpg"
    ]
  },
  "Payment": {
    "Id": 456,
    "PaymentId": "abc123",
    "Amount": "500.00",
    "PaymentDate": "2023-12-01T12:34:56Z",
    "PaymentStatus": "Completed"
  }
}

UpdateBooking
HTTP Method: PUT
Route: api/bookings/update/{bookingId}
Description: Updates an existing booking.

Parameters
bookingUpdateDTO (body): Details of the booking to be updated.
bookingId (route): ID of the booking to be updated.
Request Body
RoomId (int): ID of the room to be booked.
CheckInDate (DateTime): New check-in date.
CheckOutDate (DateTime): New check-out date.
Responses
200 OK: Returns a BookingDTO.
401 Unauthorized: Returns an error message.
403 Forbidden: Returns an error message.
404 Not Found: Returns an error message.
500 Internal Server Error: Returns an error message.

Example Request:
{
  "RoomId": 1,
  "CheckInDate": "2023-12-02T00:00:00Z",
  "CheckOutDate": "2023-12-06T00:00:00Z"
}

Example Response:
{
  "Id": 123,
  "Amount": "600.00",
  "ConfirmedByAdmin": false,
  "IsPaid": true,
  "IsCancelled": false,
  "PaymentType": "CreditCard",
  "CheckInDate": "2023-12-02T00:00:00Z",
  "CheckOutDate": "2023-12-06T00:00:00Z",
  "Room": {
    "Id": 1,
    "RoomNumber": 101,
    "RoomType": "Deluxe",
    "RoomClass": "Suite",
    "Description": "A deluxe suite with a view",
    "Amount": "150.00",
    "IsAvailable": true,
    "Photos": [
      "photo1.jpg",
      "photo2.jpg"
    ]
  },
  "Payment": {
    "Id": 456,
    "PaymentId": "abc123",
    "Amount": "600.00",
    "PaymentDate": "2023-12-02T12:34:56Z",
    "PaymentStatus": "Completed"
  }
}

CancelBooking
HTTP Method: PUT
Route: api/bookings/cancel/{bookingId}
Description: Cancels an existing booking.

Parameters
bookingId (route): ID of the booking to be canceled.
Responses
200 OK: Booking canceled successfully.
400 Bad Request: Returns an error message.
401 Unauthorized: Returns an error message.
403 Forbidden: Returns an error message.
404 Not Found: Returns an error message.
500 Internal Server Error: Returns an error message.

Example Request:
PUT /api/bookings/cancel/123

Example Response:
{
  "message": "Booking canceled successfully."
}

Data Transfer Objects (DTOs)
BookingCreateDTO
Represents the details required to create a new booking.

RoomId (int): ID of the room to be booked.
PaymentType (PaymentType): Type of payment.
CheckInDate (DateTime): Check-in date.
CheckOutDate (DateTime): Check-out date.
BookingDTO
Represents the details of a booking.

Id (int): Booking ID.
Amount (string): Booking amount.
ConfirmedByAdmin (bool): Whether the booking is confirmed by an admin.
IsPaid (bool): Whether the booking is paid.
IsCancelled (bool): Whether the booking is canceled.
PaymentType (string): Type of payment.
CheckInDate (DateTime): Check-in date.
CheckOutDate (DateTime): Check-out date.
Room (RoomDTO): Room details.
Payment (PaymentDTO?): Payment details (optional).
BookingUpdateDTO
Represents the details required to update an existing booking.

RoomId (int): ID of the room to be booked.
CheckInDate (DateTime): New check-in date.
CheckOutDate (DateTime): New check-out date.
RoomDTO
Represents the details of a room.

Id (int): Room ID.
RoomNumber (int): Room number.
RoomType (string): Room type.
RoomClass (string): Room class.
Description (string?): Room description (optional).
Amount (string): Room cost.
IsAvailable (bool): Room availability status.
Photos (ICollection<string>?): Collection of room photos (optional).
PaymentDTO
Represents the details of a payment.

Id (int): Payment ID.
PaymentId (string): Payment identifier.
Amount (string): Payment amount.
PaymentDate (DateTime): Payment date.
PaymentStatus (string): Payment status.
Logging
Errors are logged using the ILogger service with the error message and exception details.

Exception Handling
All actions have try-catch blocks to handle exceptions and log errors. If an exception occurs, a 500 Internal Server Error response is returned with the exception message.