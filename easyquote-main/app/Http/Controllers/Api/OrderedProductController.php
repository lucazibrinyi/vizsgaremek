<?php

namespace App\Http\Controllers\Api;

use App\Http\Controllers\Controller;
use App\Models\OrderedProduct;
use App\Http\Requests\StoreOrderedProductRequest;
use App\Http\Requests\UpdateOrderedProductRequest;
use App\Http\Resources\OrderedProductResource;
use App\Http\Resources\OrderResource;
use App\Models\Order;
use App\Models\Product;
use Illuminate\Support\Facades\Auth;

class OrderedProductController extends Controller
{
    /**
     * Display a listing of the resource.
     */
    public function index()
    {
        if ($this->employee()) {
            $ordered = OrderedProduct::orderByDesc('id')->get();
        }
        return OrderedProductResource::collection($ordered);
    }

    public function employee()
    {
        $user = Auth::user();
        return $user->employee;
    }

    public function orderedProductList($id)
    {
        if ($this->employee()) {
            $finalList = OrderedProduct::where('order_id', $id)->get();
        } else {
            $loggedInUserId = Auth::id();
            $list = OrderedProduct::where('order_id', $id)->get();
            $finalList = [];
            foreach ($list as $item) {
                $order = Order::find($item->order_id);
                if ($order && $order->user->id === $loggedInUserId) {
                    $finalList[] = $item;
                }
            }
        }
        return OrderedProductResource::collection($finalList);
    }


    /**
     * Store a newly created resource in storage.
     */
    public function store(StoreOrderedProductRequest $request)
    {
        if ($this->employee()) {
            $validatedData = $request->validated();

            $product = Product::findOrFail($validatedData['product_id']);
            $productPrice = $product->price;

            $orderedProductPrice = $validatedData['price'] / $validatedData['quantity'];

            if (abs($orderedProductPrice - $productPrice) > 0.001) {
                return response()->json(['error' => 'Invalid product price'], 400);
            }

            $existingOrderedProduct = OrderedProduct::where('order_id', $validatedData['order_id'])
                ->where('product_id', $validatedData['product_id'])
                ->first();

            if ($existingOrderedProduct) {
                $existingOrderedProduct->update([
                    'quantity' => $existingOrderedProduct->quantity + $validatedData['quantity'],
                    'price' => $existingOrderedProduct->price + $validatedData['price']
                ]);
                return response(['data' => new OrderedProductResource($existingOrderedProduct)], 201);
            }

            $ordered = OrderedProduct::create($validatedData);

            return response(['data' => new OrderedProductResource($ordered)], 201);
        }
    }

    /**
     * Display the specified resource.
     */
    public function show(OrderedProduct $orderedProduct)
    {
        return new OrderResource($orderedProduct);
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(UpdateOrderedProductRequest $request, OrderedProduct $orderedProduct)
    {
        if ($this->employee()) {
            $data = $request->validated();
            $orderedProduct->update($data);

            return new OrderResource($orderedProduct);
        }
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(OrderedProduct $orderedProduct)
    {
        if ($this->employee()) {
            $orderedProduct->delete();
            return response("A termék kiválasztása sikeresen törölve", 204);
        }
    }
}
