# --------------------------------------------------------------
# HOTEL PRICE SCRAPER (Question 4)
# --------------------------------------------------------------
# This program scrapes hotel room data from two demo websites
# that legally allow web scraping. It extracts room names,
# prices, stores them into a CSV file, and finally prints the
# data again from the CSV.
# --------------------------------------------------------------

import csv
import requests
from bs4 import BeautifulSoup

# Function to scrape hotel webpage
def scrape_site(url):
    rooms = []
    
    response = requests.get(url)
    soup = BeautifulSoup(response.text, "html.parser")

    # Selecting room cards (from product_pod class)
    products = soup.select(".product_pod")

    # Extract first 5 rooms
    for item in products[:5]:
        title = item.h3.a["title"]
        price = item.select_one(".price_color").text

        rooms.append([title, price])
    
    return rooms


# URLs of sample hotel pages (allowed for scraping)
hotel1_url = "https://booking-hotels2.tiiny.site/"
hotel2_url = "https://hotel1.tiiny.site/"

hotel1_data = scrape_site(hotel1_url)
hotel2_data = scrape_site(hotel2_url)

# CSV file name
csv_filename = "hotel_prices.csv"

# --------------------------------------------------------------
# Writing scraped data to CSV file
# --------------------------------------------------------------
with open(csv_filename, "w", newline="", encoding="utf-8") as file:
    writer = csv.writer(file)
    writer.writerow(["Hotel", "Room Name", "Price", "Season Dates"])

    season = "20–30 December"

    for room in hotel1_data:
        writer.writerow(["Hotel A", room[0], room[1], season])

    for room in hotel2_data:
        writer.writerow(["Hotel B", room[0], room[1], season])

print(f"\nData saved in {csv_filename}\n")

# --------------------------------------------------------------
# Reading data from CSV and displaying in terminal
# --------------------------------------------------------------
print("Retrieved Data from CSV:\n")

with open(csv_filename, "r", encoding="utf-8") as file:
    reader = csv.reader(file)
    for row in reader:
        print(row)
