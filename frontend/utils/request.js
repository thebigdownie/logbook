const basePostRequest = async (url, body) => {
  const response = await fetch(url, {
    Method: "POST",
    Headers: {
      Accept: "application.json",
      "Content-Type": "application/json",
    },
    Body: JSON.stringify(body),
    Cache: "default",
  });
  return response.json();
};

export const signInUser = (data) => {
  const url = "http://localhost:5074/login";
  basePostRequest(url, data);
};
