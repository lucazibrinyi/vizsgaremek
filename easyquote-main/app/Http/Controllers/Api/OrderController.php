<?php

namespace App\Http\Controllers\Api;

use App\Http\Controllers\Controller;
use App\Models\Order;
use App\Http\Requests\StoreOrderRequest;
use App\Http\Requests\UpdateOrderRequest;
use App\Http\Resources\OrderResource;
use Illuminate\Support\Facades\Auth;

class OrderController extends Controller
{
    /**
     * Display a listing of the resource.
     */
    public function index()
    {
        if ($this->employee()) {
            $order = Order::where('accepted', 1)
                ->orderByDesc('id')
                ->get();
        } else {
            $user_id = Auth::id();
            $order = Order::where('user_id', $user_id)
                ->where('accepted', 1)
                ->orderBy('id', 'DESC')
                ->get();
        }

        return OrderResource::collection($order);
    }

    public function employee()
    {
        $user = Auth::user();
        return $user->employee;
    }

    public function openOrders()
    {
        if ($this->employee()) {
            $order = Order::where('survey', '0')
                ->where('accepted', 1)
                ->get();
        }
        return OrderResource::collection($order);
    }

    /**
     * Store a newly created resource in storage.
     */
    public function store(StoreOrderRequest $request)
    {

        $validatedData = $request->validated();
        $order = Order::create($validatedData);

        return response(['data' => new OrderResource($order)], 201);
    }

    /**
     * Display the specified resource.
     */
    public function show(Order $order)
    {
        /*
        if ($this->employee()) {
            return new OrderResource($order);
        }
        */
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(UpdateOrderRequest $request, Order $order)
    {
        /*
        if ($this->employee()) {
            $data = $request->validated();
            $order->update($data);
            return new OrderResource($order);
        }
        */
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(Order $order)
    {
        /*
        if ($this->employee()) {
            $order->delete();
            return response("A rendelés sikeresen törölve", 204);
        }
        */
    }
}
