class User {
  final int id;
  final String email;
  final String fullName;
  final String userType;
  final String? phone;
  final String? profileImage;
  final String? bio;
  final String token;

  User({
    required this.id,
    required this.email,
    required this.fullName,
    required this.userType,
    this.phone,
    this.profileImage,
    this.bio,
    required this.token,
  });

  factory User.fromJson(Map<String, dynamic> json) {
    return User(
        id: json['id'],
        email: json['email'],
        fullName: json['fullName'],
        userType: json['userType'],
        phone: json['phone'],
        profileImage: json['profileImage'],
        bio: json['bio'],
        token: json['token']);
  }
  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'email': email,
      'fullName': fullName,
      'userType': userType,
      'phone': phone,
      'profileImage': profileImage,
      'bio': bio,
      'token': token,
    };
  }
}
