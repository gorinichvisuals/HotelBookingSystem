global using Microsoft.AspNetCore.Mvc;
global using Swashbuckle.AspNetCore.Annotations;
global using Microsoft.AspNetCore.RateLimiting;
global using System.Security.Claims;
global using Microsoft.AspNetCore.Authorization;

global using HotelBooking.Api.Services;

global using HotelBooking.Infrastructure.Config;
global using HotelBooking.Database.Config;

global using HotelBooking.Application.Config;
global using HotelBooking.Application.Services.Abstractions;
global using HotelBooking.Application.DTO.Customer;
global using HotelBooking.Application.DTO.Auth;
global using HotelBooking.Application.DTO.Room;
global using HotelBooking.Application.DTO.Booking;
global using HotelBooking.Application.DTO.Payment;

global using Shared.Services.Config;
global using Shared.Services.Constants;
global using Shared.Services.Enums.User;

global using Mono.PaymentService.Config;
global using Mono.PaymentService.DTO.Responses;