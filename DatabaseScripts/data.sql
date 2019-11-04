SET IDENTITY_INSERT [dbo].[Course] ON
INSERT INTO [dbo].[Course] ([Id], [CourseName], [Credits]) VALUES (1, N'Java Web Application development', 50)
INSERT INTO [dbo].[Course] ([Id], [CourseName], [Credits]) VALUES (2, N'C# Web application development', 50)
INSERT INTO [dbo].[Course] ([Id], [CourseName], [Credits]) VALUES (3, N'Cloud computing with AWS', 60)
SET IDENTITY_INSERT [dbo].[Course] OFF
SET IDENTITY_INSERT [dbo].[Student] ON
INSERT INTO [dbo].[Student] ([Id], [StudentName], [ContactNumber]) VALUES (1, N'Harry McDonald', N'02134567444')
INSERT INTO [dbo].[Student] ([Id], [StudentName], [ContactNumber]) VALUES (2, N'Thomas Campbell', N'02134445678')
SET IDENTITY_INSERT [dbo].[Student] OFF
SET IDENTITY_INSERT [dbo].[Enrollment] ON
INSERT INTO [dbo].[Enrollment] ([Id], [CourseId], [StudentId], [Semester]) VALUES (1, 1, 1, 2)
INSERT INTO [dbo].[Enrollment] ([Id], [CourseId], [StudentId], [Semester]) VALUES (2, 2, 2, 0)
SET IDENTITY_INSERT [dbo].[Enrollment] OFF
SET IDENTITY_INSERT [dbo].[Result] ON
INSERT INTO [dbo].[Result] ([Id], [CourseId], [StudentId], [Grade]) VALUES (1, 1, 1, 0)
INSERT INTO [dbo].[Result] ([Id], [CourseId], [StudentId], [Grade]) VALUES (2, 2, 2, 0)
SET IDENTITY_INSERT [dbo].[Result] OFF
INSERT INTO [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'9aed26f2-2dac-4c3a-a351-a6b869a14b2b', N'admin@studentmanagement.com', N'ADMIN@STUDENTMANAGEMENT.COM', N'admin@studentmanagement.com', N'ADMIN@STUDENTMANAGEMENT.COM', 0, N'AQAAAAEAACcQAAAAEI6Jt7bwmhC+t6kjAYa9l5iTvx4E4/V4N5/c9Hl4zSX5+QPrAEuh1Degqm0vyTSo/Q==', N'NVO3PQ65XLSIR7PXKZF52BOBC7UPWEHE', N'0590bc3f-51a0-4f17-8066-9d13ef7ade23', NULL, 0, 0, NULL, 1, 0)

