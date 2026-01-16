import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:job_recruitment_app/constants/colors.dart';
import 'package:job_recruitment_app/providers/auth_provider.dart';
import 'package:job_recruitment_app/widgets/custom_button.dart';
import 'package:job_recruitment_app/widgets/custom_textfield.dart';
import 'package:fluttertoast/fluttertoast.dart';

class RegisterScreen extends StatefulWidget {
  const RegisterScreen({Key? key}) : super(key: key);

  @override
  _RegisterScreenState createState() => _RegisterScreenState();
}

class _RegisterScreenState extends State<RegisterScreen> {
  final _formKey = GlobalKey<FormState>();
  final _emailController = TextEditingController();
  final _passwordController = TextEditingController();
  final _fullNameController = TextEditingController();
  final _phoneController = TextEditingController();
  String _userType = 'Seeker';

  @override
  Widget build(BuildContext context) {
    final authProvider = Provider.of<AuthProvider>(context);

    return Scaffold(
      body: SafeArea(
        child: SingleChildScrollView(
          padding: const EdgeInsets.all(20),
          child: Form(
            key: _formKey,
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                const SizedBox(height: 40),
                const Text(
                  'Create Account',
                  style: TextStyle(
                    fontSize: 32,
                    fontWeight: FontWeight.bold,
                    color: AppColors.primary,
                  ),
                ),
                const SizedBox(height: 8),
                const Text(
                  'Join us today!',
                  style: TextStyle(
                    fontSize: 16,
                    color: AppColors.gray,
                  ),
                ),
                const SizedBox(height: 40),
                CustomTextField(
                  controller: _fullNameController,
                  label: 'Full Name',
                  prefixIcon: Icons.person,
                ),
                const SizedBox(height: 20),
                CustomTextField(
                  controller: _emailController,
                  label: 'Email',
                  keyboardType: TextInputType.emailAddress,
                  prefixIcon: Icons.email,
                ),
                const SizedBox(height: 20),
                CustomTextField(
                  controller: _passwordController,
                  label: 'Password',
                  obscureText: true,
                  prefixIcon: Icons.lock,
                ),
                const SizedBox(height: 20),
                CustomTextField(
                  controller: _phoneController,
                  label: 'Phone (Optional)',
                  keyboardType: TextInputType.phone,
                  prefixIcon: Icons.phone,
                ),
                const SizedBox(height: 20),
                Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    const Text(
                      'I want to join as:',
                      style: TextStyle(
                        color: AppColors.gray,
                        fontSize: 14,
                      ),
                    ),
                    const SizedBox(height: 8),
                    Row(
                      children: [
                        Expanded(
                          child: ChoiceChip(
                            label: const Text('Job Seeker'),
                            selected: _userType == 'Seeker',
                            onSelected: (selected) {
                              setState(() {
                                _userType = 'Seeker';
                              });
                            },
                            selectedColor: AppColors.primary,
                            labelStyle: TextStyle(
                              color: _userType == 'Seeker'
                                  ? Colors.white
                                  : AppColors.gray,
                            ),
                          ),
                        ),
                        const SizedBox(width: 10),
                        Expanded(
                          child: ChoiceChip(
                            label: const Text('Recruiter'),
                            selected: _userType == 'Recruiter',
                            onSelected: (selected) {
                              setState(() {
                                _userType = 'Recruiter';
                              });
                            },
                            selectedColor: AppColors.primary,
                            labelStyle: TextStyle(
                              color: _userType == 'Recruiter'
                                  ? Colors.white
                                  : AppColors.gray,
                            ),
                          ),
                        ),
                      ],
                    ),
                  ],
                ),
                const SizedBox(height: 30),
                CustomButton(
                  text: 'Sign Up',
                  isLoading: authProvider.isLoading,
                  onPressed: () async {
                    if (_formKey.currentState!.validate()) {
                      final success = await authProvider.register(
                        email: _emailController.text.trim(),
                        password: _passwordController.text.trim(),
                        fullName: _fullNameController.text.trim(),
                        userType: _userType,
                        phone: _phoneController.text.trim(),
                      );
                      
                      if (success) {
                        Fluttertoast.showToast(
                          msg: 'Registration successful!',
                          toastLength: Toast.LENGTH_SHORT,
                          gravity: ToastGravity.BOTTOM,
                          backgroundColor: AppColors.success,
                        );
                      } else {
                        Fluttertoast.showToast(
                          msg: 'Registration failed!',
                          toastLength: Toast.LENGTH_SHORT,
                          gravity: ToastGravity.BOTTOM,
                          backgroundColor: AppColors.danger,
                        );
                      }
                    }
                  },
                ),
                const SizedBox(height: 20),
                Center(
                  child: TextButton(
                    onPressed: () {
                      Navigator.pop(context);
                    },
                    child: RichText(
                      text: const TextSpan(
                        text: 'Already have an account? ',
                        style: TextStyle(color: AppColors.gray),
                        children: [
                          TextSpan(
                            text: 'Sign In',
                            style: TextStyle(
                              color: AppColors.primary,
                              fontWeight: FontWeight.bold,
                            ),
                          ),
                        ],
                      ),
                    ),
                  ),
                ),
              ],
            ),
          ),
        ),
      ),
    );
  }

  @override
  void dispose() {
    _emailController.dispose();
    _passwordController.dispose();
    _fullNameController.dispose();
    _phoneController.dispose();
    super.dispose();
  }
}