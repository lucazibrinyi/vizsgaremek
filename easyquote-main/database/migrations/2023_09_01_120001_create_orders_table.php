<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\DB;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    /**
     * Run the migrations.
     */
    public function up(): void
    {
        Schema::create('orders', function (Blueprint $table) {
            $table->id();
            $table->unsignedBigInteger('user_id')->nullable();
            $table->foreign('user_id')->references('id')->on('users')->onDelete('SET NULL');
            $table->string('postal_code', 6);
            $table->string('city', 100);
            $table->string('address', 100);
            $table->string('phone_number', 50);
            $table->date('payment_deadline')->default(null)->nullable();
            $table->boolean('accepted')->default(false);
            $table->boolean('survey')->default(false);
            $table->boolean('completed')->default(false);
            $table->timestamps();
        });

        DB::table('orders')->insert([
            [
                'user_id' => '6',
                'postal_code' => '1000',
                'city' => 'Százholdas Pagony',
                'address' => 'Faház út',
                'phone_number' => '06201234567',
                'created_at' => '2023-01-05 00:00:00',
                'updated_at' => '2023-01-05 00:00:00',
            ],
            [
                'user_id' => '7',
                'postal_code' => '1500',
                'city' => 'Százholdas Pagony',
                'address' => 'Faodu utca',
                'phone_number' => '06301234567',
                'created_at' => '2023-01-06 00:00:00',
                'updated_at' => '2023-01-06 00:00:00',
            ],
            [
                'user_id' => '8',
                'postal_code' => '2000',
                'city' => 'Százholdas Pagony',
                'address' => 'Sátoralja köz',
                'phone_number' => '06701234567',
                'created_at' => '2023-01-07 00:00:00',
                'updated_at' => '2023-01-07 00:00:00',
            ],
        ]);
    }

    /**
     * Reverse the migrations.
     */
    public function down(): void
    {
        Schema::dropIfExists('orders');
    }
};
