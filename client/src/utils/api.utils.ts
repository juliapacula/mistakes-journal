export const get = async (url: string) => {
  const response = await fetch(url, {
    method: 'GET',
  });

  if (!response.ok) {
    throw response;
  }

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

  if (!response.ok) {
    throw response;
  }

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

  if (!response.ok) {
    throw response;
  }

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

  if (!response.ok) {
    throw response;
  }

  try {
    return await response.json();
  } catch {
    return null;
  }
};
