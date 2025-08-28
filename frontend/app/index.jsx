import { Redirect } from "expo-router";

export default function Index() {
  const isLoggedIn = false; // replace with auth state

  if (isLoggedIn) {
    return <Redirect href="/register" />;
  } else {
    return <Redirect href="/login" />;
  }
}
