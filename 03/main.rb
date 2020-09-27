require 'bitcoin'
require 'rest-client'
require 'json'

include Bitcoin::Builder
Bitcoin.network = :testnet3

BITCOIN_TRANSACTIONS_PUBLIC_KEY  = '0382d926256cee53a94ff3e2ffc882de17f45209ac1b93150b14d53efdb80b10c5'
BITCOIN_TRANSACTIONS_PRIVATE_KEY = 'Kx8Bivqvyb8nR4HcsxZXkxfGiY4eckZnXuqzZjtt2EvMRdyryr1T'
MY_ADDRESS = 'mxknA2bAcSSLxPUDvNhMuzdfHkFw8isrNo'
send_to_me = 50000

faucet_address = 'mkHS9ne12qx9pS9VojpwU5xtRd4T7X7ZUt'

send_to = 'mr9xV9aNnfv6a1LhGsvVmFGW63Mn3ZwtJz'
send_value = 250000

utxo = 'b50b86d9f16ffbb20f3b59b6201ff4d77fcf7782c5de30275d7299ef29ba3fe3'
utxo_value = 1000000
utxo_index = 0

bitcoin_fee_per_byte = 52

key = Bitcoin::Key.new(BITCOIN_TRANSACTIONS_PRIVATE_KEY, BITCOIN_TRANSACTIONS_PUBLIC_KEY)

response = RestClient.get("https://sochain.com/api/v2/get_tx/BTCTEST/#{utxo}")
parsed_response = JSON.parse(response)
prev_tx = Bitcoin::P::Tx.new(parsed_response['data']['tx_hex'])

new_tx = build_tx do |t|
  t.input do |i|
    i.prev_out prev_tx
    i.prev_out_index utxo_index
    i.signature_key key
  end

  t.output do |o|
    o.value send_value
    o.script { |s| s.type :address; s.recipient send_to }
  end

  t.output do |o|
    o.value send_to_me
    o.script { |s| s.type :address; s.recipient MY_ADDRESS }
  end

  t.output do |o|
    o.value utxo_value - send_value - fee
    o.script { |s| s.type :address; s.recipient faucet_address }
  end
end

fee = new_tx.size * bitcoin_fee_per_byte

rawtx = new_tx.to_payload.unpack('H*').first
response = RestClient.post("https://sochain.com/api/v2/send_tx/BTCTEST", { tx_hex: rawtx })
puts JSON.parse(response)
