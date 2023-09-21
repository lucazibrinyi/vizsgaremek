<?php

namespace App\Http\Controllers\Api;

use App\Http\Controllers\Controller;
use App\Models\Product;
use App\Http\Requests\StoreProductRequest;
use App\Http\Requests\UpdateProductRequest;
use App\Http\Resources\ProductResource;
use Illuminate\Support\Facades\Auth;

class ProductController extends Controller
{
    /**
     * Display a listing of the resource.
     */
    public function index()
    {
        if ($this->employee()) {
            return ProductResource::collection(Product::where('active', 1)
            ->get());
        }
    }

    public function employee()
    {
        $user = Auth::user();
        return $user->employee;
    }

    public function searchByName($name)
    {
        if ($this->employee()) {
            $products = Product::where('name', 'like', '%' . $name . '%')
                ->where('active', 1)
                ->get();
            return ProductResource::collection($products);
        }
    }

    public function searchByCategory($category)
    {
        if ($this->employee()) {
            $products = Product::where('category', 'like', '%' . $category . '%')
                ->where('active', 1)
                ->get();
            return ProductResource::collection($products);
        }
    }

    public function searchBySubCategory($subCategory)
    {
        if ($this->employee()) {
            $products = Product::where('sub_category', 'like', '%' . $subCategory . '%')
                ->where('active', 1)
                ->get();
            return ProductResource::collection($products);
        }
    }

    /**
     * Store a newly created resource in storage.
     */
    public function store(StoreProductRequest $request)
    {
        /*
        $validatedData = $request->validated();
        $product = Product::create($validatedData);

        return response(['data' => new ProductResource($product)], 201);
        */
    }

    /**
     * Display the specified resource.
     */
    public function show(Product $product)
    {
        return new ProductResource($product);
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(UpdateProductRequest $request, Product $product)
    {
        /*
        $data = $request->validated();
        $product->update($data);

        return new ProductResource($product);
        */
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(Product $product)
    {
        /*
        $product->delete();

        return response("A termék sikeresen törölve", 204);
        */
    }
}
