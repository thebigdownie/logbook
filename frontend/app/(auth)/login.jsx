import React, { useState } from "react";
import { Pressable, StyleSheet, Text, TextInput, View } from "react-native";
import { Link } from "expo-router";

const Login = () => {
  const [username, setUsername] = useState(null);
  const [password, setPassword] = useState(null);

  const submitLoginDetails = () => {
    const loginPayload = { username, password };
  };

  return (
    <View style={styles.container}>
      <TextInput
        style={{ width: "80%", marginBottom: 20 }}
        placeholder="Username"
        placeholderTextColor="red"
        onChange={setUsername}
        value={username}
      />

      <TextInput
        style={{ width: "80%", marginBottom: 20 }}
        placeholder="Password"
        placeholderTextColor="red"
        onChange={setPassword}
        value={password}
      />

      <Pressable onPress={() => {}}>
        <Text>Sign in</Text>
      </Pressable>

      <Link href="/register">
        <Text>register</Text>
      </Link>
    </View>
  );
};

export default Login;

const styles = StyleSheet.create({
  container: {
    flex: 0.5,
    justifyContent: "center",
    alignItems: "center",
  },
});
