<?php

namespace App\Http\Controllers\Api;

use App\Http\Controllers\Controller;
use Illuminate\Http\Request;

class PhotoController extends Controller
{
    public function upload(Request $request)
    {
        $uploadedFile = $request->file('photo');
        $filename = $uploadedFile->getClientOriginalName();
        $uploadedFile->move(public_path('images'), $filename);
    }
}
