Description
CleverAlbumDesigner is a ASP.NET Core web application designed to organize photo collections by generating customized photo albums. The application analyzes uploaded photos, identifies their dominant colors, and categorizes them into themed albums based on predefined color schemes and user-selected themes.

Key Features

Photo Upload and Storage:
Users can upload photos, which are securely stored in Amazon S3 buckets and their base information in a SQL Database.

Dominant Color Analysis:
The system analyzes each photo to detect its dominant color using image processing techniques.

Theme-Based Album Generation:
Photos are grouped into albums based on themes using a predefined color palette.

Real-Time Processing:
Instant feedback and results when uploading photos or generating albums.

Dynamic API:
A fully functional RESTful API facilitates communication between the backend and frontend.
Endpoints provide access to photo information, album generation, and storage integration.

Scalability and Cloud Integration:
Designed to take advantage of cloud services like Amazon S3 for storage and Amazon RDS (SQL Server) for database management.
Deployed on Amazon EC2.
