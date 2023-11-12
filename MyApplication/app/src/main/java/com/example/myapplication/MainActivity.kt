package com.example.myapplication

import android.content.Intent
import android.os.Bundle
import android.widget.Button
import android.widget.EditText
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

class MainActivity : AppCompatActivity() {

    private lateinit var editTextUsername: EditText
    private lateinit var editTextPassword: EditText
    private lateinit var loginButton: Button
    private lateinit var registerButton: Button

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        editTextUsername = findViewById(R.id.editTextUsername)
        editTextPassword = findViewById(R.id.editTextPassword)
        loginButton = findViewById(R.id.loginButton)
        registerButton = findViewById(R.id.registerButton)

        loginButton.setOnClickListener {
            val username = editTextUsername.text.toString()
            val password = editTextPassword.text.toString()

            loginUser(username, password)
        }

        registerButton.setOnClickListener {
            val intent = Intent(this@MainActivity, RegisterActivity::class.java)
            startActivity(intent)
        }
    }

    private fun loginUser(username: String, password: String) {
        val apiService: ApiService = ApiClient.getClient().create(ApiService::class.java)
        val call: Call<AuthTokenResponse> = apiService.loginUser(LoginModel(username, password))

        call.enqueue(object : Callback<AuthTokenResponse> {
            override fun onResponse(call: Call<AuthTokenResponse>, response: Response<AuthTokenResponse>) {
                if (response.isSuccessful) {
                    val authTokenResponse: AuthTokenResponse? = response.body()
                    val intent = Intent(this@MainActivity, HomeActivity::class.java)
                    startActivity(intent)
                    finish()
                } else {
                    Toast.makeText(this@MainActivity, "Невірне ім'я користувача або пароль", Toast.LENGTH_SHORT).show()
                }
            }

            override fun onFailure(call: Call<AuthTokenResponse>, t: Throwable) {
                Toast.makeText(this@MainActivity, "Помилка під час входу", Toast.LENGTH_SHORT).show()
            }
        })
    }
}
