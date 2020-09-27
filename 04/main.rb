require 'ethereum.rb'

client = Ethereum::IpcClient.new("ethereum_testnet/geth.ipc")

init = Ethereum::Initializer.new("sicknote.sol", client)
init.build_all

check_presence_instance = SimpleNameRegistry.new('Abby', 'Concussion', '09-27-2020')
check_presence_instance.getPatient
check_presence_instance.getDiagnosis
check_presence_instance.getDate
