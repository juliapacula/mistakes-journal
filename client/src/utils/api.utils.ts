export const get = async (url: string) => {
  const response = await fetch(url, {
    method: 'GET',
  });
  try {
    return await response.json();
  } catch {
    return null;
  }
};

export const post = async (url: string, body: any) => {
  const response = await fetch(url, {
    body: JSON.stringify(body),
    headers: {
      'Content-Type': 'application/json',
    },
    method: 'POST',
  });

  try {
    return await response.json();
  } catch {
    return null;
  }
};

export const put = async (url: string, body: any) => {
  const response = await fetch(url, {
    body: JSON.stringify(body),
    headers: {
      'Content-Type': 'application/json',
    },
    method: 'PUT',
  });

  try {
    return await response.json();
  } catch {
    return null;
  }
};

export const remove = async (url: string) => {
  const response = await fetch(url, {
    headers: {
      'Content-Type': 'application/json',
    },
    method: 'DELETE',
  });

  try {
    return await response.json();
  } catch {
    return null;
  }
};
