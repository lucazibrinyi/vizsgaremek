<?php

namespace App\Http\Resources;

use Illuminate\Http\Request;
use Illuminate\Http\Resources\Json\JsonResource;

class OrderResource extends JsonResource
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
            'user' => new UserResource($this->user),
            'postal_code' => $this->postal_code,
            'city' => $this->city,
            'address' => $this->address,
            'phone_number' => $this->phone_number,
            'payment_deadline' => $this->payment_deadline,
            'survey' => $this->survey,
            'accepted' => $this->accepted,
            'completed' => $this->completed,
            'created_at' => $this->created_at->format('Y-m-d H:i:s'),
            'updated_at' => $this->updated_at->format('Y-m-d H:i:s'),
        ];
    }
}
