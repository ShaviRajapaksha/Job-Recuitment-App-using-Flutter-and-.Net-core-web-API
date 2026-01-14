import 'package:flutter/material.dart';
import 'package:job_recruitment_app/models/user.dart';
import 'package:job_recruitment_app/services/auth_service.dart';

class AuthProvider extends ChangeNotifier {
  User? _user;
  bool _isLoading = false;

  User? get user => _user;
  bool get isLoading => _isLoading;

  AuthProvider() {
    _loadUser();
  }

  Future<void> _loadUser() async {
    _user = await AuthService.getCurrentUser();
    notifyListeners();
  }

  Future<bool> login(String email, String password) async {
    _isLoading = true;
    notifyListeners();

    final user = await AuthService.login(email: email, password: password);
    
    _isLoading = false;
    
    if (user != null) {
      _user = user;
      notifyListeners();
      return true;
    }
    
    notifyListeners();
    return false;
  }

  Future<bool> register({
    required String email,
    required String password,
    required String fullName,
    required String userType,
    String? phone,
  }) async {
    _isLoading = true;
    notifyListeners();

    final user = await AuthService.register(
      email: email,
      password: password,
      fullName: fullName,
      userType: userType,
      phone: phone,
    );
    
    _isLoading = false;
    
    if (user != null) {
      _user = user;
      notifyListeners();
      return true;
    }
    
    notifyListeners();
    return false;
  }

  Future<void> logout() async {
    await AuthService.logout();
    _user = null;
    notifyListeners();
  }
}