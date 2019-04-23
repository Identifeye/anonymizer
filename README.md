# Anonymizer
Anonymizes our dataset by hashing all values

IP geolocation database found at https://db-ip.com/db/download/ip-to-country-lite

## Usage

The Anonymizer is a console program written in C#. It can be run using Visual Studio. It fetches data from a MySQL database. This database is specific to the third party we worked with, but a small sample data set can be created using this query: [anonymizer-sample.sql](https://github.com/Identifeye/anonymizer/blob/master/anonymizer-sample.sql)

Run the program and you will be asked to provide the url and credentials for the MySQL database. It is recommended that you either use a user with no write permissions or run the program on a backup of your database. You will then be asked to specify the number of cores to use to hash the data. The more cores you use, the quicker the program finishes.

The program will output a salt, which you should write down. This can be used to reverse-engineer the results of the analysis program on the encrypted data by comparing comparing results to the hashes of the actual users database.

For each core you use, the program takes about a third of a second to process one IP address. So if you have 100,000 IPs and are running the program with 8 cores, it will take about 1 hour and 10 minutes for it to finish. Once it is done, it will output the results in `data.csv` in your project folder.
