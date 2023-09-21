<?php

namespace App\Http\Resources;

use Illuminate\Http\Request;
use Illuminate\Http\Resources\Json\JsonResource;

class OrderedProductResource extends JsonResource
{
    /**
     * Transform the resource into an array.
     *
     * @return array<string, mixed>
     */
    public function toArray(Request $request): array
    {
        return [
            'id' => $this->id,
            'order_id' => new OrderResource($this->order),
            'product_id' => new ProductResource($this->product),
            'quantity' => $this->quantity,
            'price' => $this->price,
        ];
    }
}
