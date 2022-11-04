﻿global using System.Diagnostics;
global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;
global using System.Security.Cryptography;
global using System.Text;
global using System.Text.Json.Serialization;
global using BlazorECommerce.Server.Data;
global using BlazorECommerce.Server.Endpoints;
global using BlazorECommerce.Server.Services.AddressServices;
global using BlazorECommerce.Server.Services.AuthServices;
global using BlazorECommerce.Server.Services.CartServices;
global using BlazorECommerce.Server.Services.CategoryServices;
global using BlazorECommerce.Server.Services.OrderServices;
global using BlazorECommerce.Server.Services.PaymentServices;
global using BlazorECommerce.Server.Services.ProductServices;
global using BlazorECommerce.Server.Services.ProductTypeServices;
global using BlazorECommerce.Shared;
global using BlazorECommerce.Shared.DTOs;
global using BlazorECommerce.Shared.Models;
global using BlazorECommerce.Shared.Services;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Http.HttpResults;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.RazorPages;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.IdentityModel.Tokens;
global using Stripe;
global using Stripe.Checkout;
global using ProductService = BlazorECommerce.Server.Services.ProductServices.ProductService;
global using Address = BlazorECommerce.Shared.Models.Address;
global using Product = BlazorECommerce.Shared.Models.Product;
