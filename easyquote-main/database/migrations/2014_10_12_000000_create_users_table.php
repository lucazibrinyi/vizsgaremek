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
        Schema::create('users', function (Blueprint $table) {
            $table->id();
            $table->string('name', 150);
            $table->string('email', 150)->unique();
            $table->timestamp('email_verified_at')->nullable();
            $table->string('password');
            $table->boolean('employee')->default(false);
            $table->rememberToken();
            $table->timestamps();
        });

        DB::table('users')->insert([
            [
                'name' => 'boss',
                'email' => 'boss@easyquote.hu',
                'password' => 'df1aec32a2215ede9da14f328f370247',
                'employee' => '1',
                'created_at' => '2023-01-01 00:00:00',
                'updated_at' => '2023-01-01 00:00:00',
            ],
            [
                'name' => 'iroda1',
                'email' => 'iroda1@easyquote.hu',
                'password' => 'ad6f3a73ecfa6e953427b92c24e662ca',
                'employee' => '1',
                'created_at' => '2023-01-01 00:00:00',
                'updated_at' => '2023-01-01 00:00:00',
            ],
            [
                'name' => 'iroda2',
                'email' => 'iroda2@easyquote.hu',
                'password' => 'c802362c5e51d2538d93f7ca9dc1167d',
                'employee' => '1',
                'created_at' => '2023-01-02 00:00:00',
                'updated_at' => '2023-01-02 00:00:00',
            ],
            [
                'name' => 'iroda3',
                'email' => 'iroda3@easyquote.hu',
                'password' => '6765cf26e6fcc852a0717006d9266431',
                'employee' => '1',
                'created_at' => '2023-01-03 00:00:00',
                'updated_at' => '2023-01-03 00:00:00',
            ],
            [
                'name' => 'felmero',
                'email' => 'felmero@easyquote.hu',
                'password' => '$2y$10$VLwRmP.zPqAAfsW7S8ujOOaAIlRbaXNKUVVVe/rHkwaDH3HcakCy6',
                'employee' => '1',
                'created_at' => '2023-01-04 00:00:00',
                'updated_at' => '2023-01-04 00:00:00',
            ],
            [
                'name' => 'Micimackó',
                'email' => 'minimacko@thepooh.hu',
                'password' => '$2y$10$CVIFUTmOvMMTzZa3/C.v9.9qcunMIqahgfZre7XjTXqIENyrxLnhi',
                'employee' => '0',
                'created_at' => '2023-01-05 00:00:00',
                'updated_at' => '2023-01-05 00:00:00',
            ],
            [
                'name' => 'Malacka',
                'email' => 'malacka@theshy.hu',
                'password' => '$2y$10$o1WoYjLbVSndgrRCmjPD8OeyhLgD41JCXs3S85Bz/bzgDu05TaLIC',
                'employee' => '0',
                'created_at' => '2023-01-06 00:00:00',
                'updated_at' => '2023-01-06 00:00:00',
            ],
            [
                'name' => 'Füles',
                'email' => 'fules@thesad.hu',
                'password' => '$2y$10$79Ez8kY37LlM6niW2aEjKe9vh/LPRmKYemMTYGwY6BSkbtS2Lb5FK',
                'employee' => '0',
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
        Schema::dropIfExists('users');
    }
};
