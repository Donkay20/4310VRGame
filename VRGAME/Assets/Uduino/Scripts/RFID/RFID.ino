
#include <SPI.h>
#include <MFRC522.h>
#include<Uduino.h>
Uduino uduino("RFID"); // Declare and name your object
#define RST_PIN         9           // Configurable, see typical pin layout above
#define SS_PIN          10          // Configurable, see typical pin layout above
bool isScanned = false;
MFRC522 mfrc522(SS_PIN, RST_PIN);   // Create MFRC522 instance
void setup() {
  Serial.begin(9600);                                           // Initialize serial communications with the PC
  SPI.begin();                                                  // Init SPI bus
  mfrc522.PCD_Init();                                              // Init MFRC522 card
  Serial.println(F("---Ready"));    //shows in serial that it is ready to read
}

void loop() {
uduino.update();
 if(uduino.isConnected()){
  if ( ! mfrc522.PICC_IsNewCardPresent()) {
    return;
  }
  if ( ! mfrc522.PICC_ReadCardSerial()) {
    return;
  }
  Serial.println(F("Card Scanned"));

}
}
