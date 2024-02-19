import socket

def send_message_to_unity(message):
    try:
        # Unityがリッスンしているホストとポートに接続
        with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
            s.connect(("127.0.0.1", 8052))
            s.sendall(message.encode())
            print("Message sent to Unity")
    except ConnectionError as e:
        print(f"Connection error: {e}")

# 例えば、"start"メッセージをUnityに送る
send_message_to_unity("start")
