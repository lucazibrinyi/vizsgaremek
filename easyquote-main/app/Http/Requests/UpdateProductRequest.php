<?php

namespace App\Http\Requests;

use Illuminate\Foundation\Http\FormRequest;

class UpdateProductRequest extends FormRequest
{
    /**
     * Determine if the user is authorized to make this request.
     */
    public function authorize(): bool
    {
        return true;
    }

    /**
     * Get the validation rules that apply to the request.
     *
     * @return array<string, \Illuminate\Contracts\Validation\ValidationRule|array|string>
     */
    public function rules(): array
    {
        return [
            'name' => 'required|string|max:150',
            'category' => 'required|string|max:50',
            'sub_categor' => 'required|string|max:50',
            'img_url' => 'required|string|max:100',
            'description' => 'required|string',
            'price' => 'required|numberic',
        ];
    }
}
