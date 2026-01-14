class Job {
  final int id;
  final String title;
  final String description;
  final String company;
  final String location;
  final double salary;
  final String employmentType;
  final String? requirements;
  final String? benefits;
  final DateTime postedDate;
  final DateTime expiryDate;
  final String recruiterName;

  Job({
    required this.id,
    required this.title,
    required this.description,
    required this.company,
    required this.location,
    required this.salary,
    required this.employmentType,
    this.requirements,
    this.benefits,
    required this.postedDate,
    required this.expiryDate,
    required this.recruiterName,
  });

  factory Job.fromJson(Map<String, dynamic> json) {
    return Job(
      id: json['id'],
      title: json['title'],
      description: json['description'],
      company: json['company'],
      location: json['location'],
      salary: json['salary']?.toDouble() ?? 0.0,
      employmentType: json['employmentType'],
      requirements: json['requirements'],
      benefits: json['benefits'],
      postedDate: DateTime.parse(json['postedDate']),
      expiryDate: DateTime.parse(json['expiryDate']),
      recruiterName: json['recruiterName'],
    );
  }
}