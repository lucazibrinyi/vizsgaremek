<?php

use App\Http\Controllers\Api\AuthController;
use App\Http\Controllers\Api\OrderController;
use App\Http\Controllers\Api\OrderedProductController;
use App\Http\Controllers\Api\PhotoController;
use App\Http\Controllers\Api\ProductController;
use App\Http\Controllers\Api\UserController;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Route;

/*
|--------------------------------------------------------------------------
| API Routes
|--------------------------------------------------------------------------
|
| Here is where you can register API routes for your application. These
| routes are loaded by the RouteServiceProvider and all of them will
| be assigned to the "api" middleware group. Make something great!
|
*/

Route::middleware('auth:sanctum')->group(function () {
    Route::post('/logout', [AuthController::class, 'logout']);
    Route::get('/user', function (Request $request) {
        return $request->user();
    });
    Route::apiResource('/users', UserController::class);

    Route::apiResource('/products', ProductController::class);
    Route::get('/products/name/{name}', [ProductController::class, 'searchByName']);
    Route::get('/products/category/{category}', [ProductController::class, 'searchByCategory']);
    Route::get('/products/subCategory/{subCategory}', [ProductController::class, 'searchBySubCategory']);

    Route::apiResource('/orders', OrderController::class);
    Route::get('/openorders', [OrderController::class, 'openOrders']);

    Route::apiResource('/ordered_products', OrderedProductController::class);
    Route::get('/ordered_products/list/{id}', [OrderedProductController::class, 'orderedProductList']);
});

Route::post('/signup', [AuthController::class, 'signup']);
Route::post('/login', [AuthController::class, 'login']);

Route::post('upload', [PhotoController::class, 'upload']);
