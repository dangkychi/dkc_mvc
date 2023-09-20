
-- Quốc gia
CREATE TABLE countries (
  country_id INT PRIMARY KEY,
  country_name VARCHAR(50) NOT NULL,
  continent VARCHAR(20) NOT NULL,
  images VARCHAR(50),
);

-- Trường đại học
CREATE TABLE universities (
  university_id INT PRIMARY KEY,
  university_name VARCHAR(100) NOT NULL,
  country_id INT NOT NULL,
  tuition_fee FLOAT NOT NULL,
  location VARCHAR(100) NOT NULL,
  images VARCHAR(50),
  FOREIGN KEY (country_id) REFERENCES countries (country_id)
);

-- Chương trình học
CREATE TABLE programs (
  program_id INT PRIMARY KEY,
  program_name VARCHAR(100) NOT NULL,
  university_id INT NOT NULL,
  duration INT NOT NULL,
  description NVARCHAR (200),
  degree VARCHAR(20) NOT NULL,
  FOREIGN KEY (university_id) REFERENCES universities (university_id)
);

-- Học sinh
CREATE TABLE students (
  student_id INT PRIMARY KEY,
  student_name VARCHAR(50) NOT NULL,
  student_fname VARCHAR(50) NOT NULL,
  email VARCHAR(50) UNIQUE NOT NULL,
  password VARCHAR(50) NOT NULL,
  country_id INT NOT NULL,
  FOREIGN KEY (country_id) REFERENCES countries (country_id)
);

-- Các yêu cầu tư vấn
CREATE TABLE requests (
  request_id INT PRIMARY KEY,
  student_id INT NOT NULL,
  program_id INT NOT NULL,
  status VARCHAR(20) NOT NULL,
  created_at DATE NOT NULL,
  updated_at DATE NOT NULL,
  FOREIGN KEY (student_id) REFERENCES students (student_id),
  FOREIGN KEY (program_id) REFERENCES programs (program_id)
);

-- Nhân viên tư vấn
CREATE TABLE consultants (
  consultant_id INT PRIMARY KEY,
  consultant_name VARCHAR(50) NOT NULL,
  consultant_fname VARCHAR(50) NOT NULL,
  email VARCHAR(50) UNIQUE NOT NULL,
  password VARCHAR(50) NOT NULL
);

-- Cuộc hẹn
CREATE TABLE appointments (
  appointment_id INT PRIMARY KEY,
  request_id INT NOT NULL,
  consultant_id INT NOT NULL,
  date DATE NOT NULL,
  time TIME NOT NULL,
  FOREIGN KEY (request_id) REFERENCES requests (request_id),
  FOREIGN KEY (consultant_id) REFERENCES consultants (consultant_id));

  drop table countries
  drop table universities
  drop table programs
  drop table students
  drop table requests
  drop table consultants
  drop table appointments