CREATE TABLE Universities (
	[code] VARCHAR(255) PRIMARY KEY,
	[name] VARCHAR(255),
	[website] VARCHAR(255)
);

CREATE TABLE Careers (
	[id] INT IDENTITY (1,1) PRIMARY KEY,
	[name] VARCHAR(255),
	[description] VARCHAR(255),
	[universityCode] VARCHAR(255)
);

CREATE TABLE Courses (
	[id] INT IDENTITY (1,1) PRIMARY KEY,
	[name] VARCHAR(255),
	[cost] FLOAT,
	[careerId] INT
);

ALTER TABLE Careers ADD FOREIGN KEY (universityCode) REFERENCES Universities;
ALTER TABLE Courses ADD FOREIGN KEY (careerId) REFERENCES Careers;