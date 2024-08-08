namespace HotelBooking.Application.Services.Implementations;

public sealed class BookingService(IUnitOfWork unitOfWork) : IBookingService
{
    public async Task<ApplicationResult<BookingDTO>> CreateBooking(BookingCreateDTO bookingCreateDTO, int customerId)
    {
        RoomInternalGetDTO? room = await unitOfWork.RoomRepository
            .GetItemAsync(RoomExpressions.MapToRoomInternalDTO, b => b.Id == bookingCreateDTO.RoomId && b.IsAvailable
            && !b.Bookings!.Any(b => b.CheckInDate < bookingCreateDTO.CheckOutDate && b.CheckOutDate > bookingCreateDTO.CheckInDate));

        if (room is null)
            return ApplicationResult<BookingDTO>.Fail(BookingStatusCodes.NotFound, "Room is not found or unavailable!");

        Booking newBooking = new()
        {
            CustomerId = customerId,
            RoomId = bookingCreateDTO.RoomId,
            Amount = room.Amount,          
            PaymentType = bookingCreateDTO.PaymentType,
            CheckInDate = bookingCreateDTO.CheckInDate,
            CheckOutDate = bookingCreateDTO.CheckOutDate,
        };

        await unitOfWork.BookingRepository.CreateAsync(newBooking);
        await unitOfWork.SaveChanges();

        BookingDTO? bookingDTO = await unitOfWork.BookingRepository
            .GetItemAsync(BookingExpressions.MapToBookingDTO, b => b.Id == newBooking.Id);

        return ApplicationResult<BookingDTO>.Success(BookingStatusCodes.Created, bookingDTO!);
    }

    public async Task<ApplicationResult<BookingDTO>> UpdateBooking(BookingUpdateDTO bookingUpdateDTO, int bookingId)
    {
        bool bookingExists = await unitOfWork.BookingRepository.AnyAsync(b => b.Id == bookingId);

        if (!bookingExists)
            return ApplicationResult<BookingDTO>.Fail(BookingStatusCodes.NotFound, "Booking is not found.");

        RoomInternalGetDTO? room = await unitOfWork.RoomRepository
            .GetItemAsync(RoomExpressions.MapToRoomInternalDTO, b => b.Id == bookingUpdateDTO.RoomId && b.IsAvailable
            && !b.Bookings!.Any(b => b.CheckInDate < bookingUpdateDTO.CheckOutDate && b.CheckOutDate > bookingUpdateDTO.CheckInDate));

        if (room is null)
            return ApplicationResult<BookingDTO>.Fail(BookingStatusCodes.NotFound, "Room is not found or unavailable!");

        await unitOfWork.BookingRepository.UpdateBooking(b => b.Id == bookingId, bookingUpdateDTO.RoomId, room.Amount, 
            bookingUpdateDTO.CheckInDate, bookingUpdateDTO.CheckOutDate);

        BookingDTO? bookingDTO = await unitOfWork.BookingRepository
            .GetItemAsync(BookingExpressions.MapToBookingDTO, b => b.Id == bookingId);

        return ApplicationResult<BookingDTO>.Success(BookingStatusCodes.Ok, bookingDTO!);
    }

    public async Task<ApplicationResult> CancelBooking(int bookingId)
    {
        bool bookingExists = await unitOfWork.BookingRepository.AnyAsync(b => b.Id == bookingId);

        if (!bookingExists)
            return ApplicationResult.Fail(BookingStatusCodes.NotFound, "Booking is not found.");

        bool isPaid = await unitOfWork.BookingRepository.GetItemAsync(b => b.IsPaid, b => b.Id == bookingId);

        if (isPaid)
            return ApplicationResult.Fail(BookingStatusCodes.BadRequest, "Booking was paid. Impossible to cancel!");

        await unitOfWork.BookingRepository.CancelBooking(b => b.Id == bookingId);

        return ApplicationResult.Success(BookingStatusCodes.Ok);
    }
}